using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NewSmartApp.Webservice.Models;
using NewSmartApp.Webservice.Models.Response;
using NewSmartApp.Webservice.Models.Subscription;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace NewSmartApp.Webservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewSmartAppController : ControllerBase
    {
        readonly INodeServices nodeServices;
        readonly IConfiguration configuration;
        readonly IHostingEnvironment environment;
        readonly ILogger<NewSmartAppController> logger;

        public NewSmartAppController(INodeServices nodeServices, IConfiguration configuration, IHostingEnvironment environment, ILogger<NewSmartAppController> logger)
        {
            this.nodeServices = nodeServices;
            this.configuration = configuration;
            this.environment = environment;
            this.logger = logger;
        }
        // GET api/NewSmartApp
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() => new string[] { "value1", "value2" };

        // GET api/NewSmartApp/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) => "value";

        // POST api/NewSmartApp
        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string authorization, [FromHeader] string digest, [FromHeader] string date)
        {
            StreamReader reader = new StreamReader(HttpContext.Request.Body);
            string requestBody = reader.ReadToEnd();
            //Debug.Print($"\r\n*****\r\n{requestBody}\r\n****\r\n");
            LifecycleBase lifecycle = JsonConvert.DeserializeObject<LifecycleBase>(requestBody);
            IActionResult result;

            if (lifecycle.Lifecycle.Equals("PING"))
            {
                result = ProcessPingLifecycle(requestBody);
            }
            else
            {
                result = await ProcessLifecycleIfVerified(lifecycle.Lifecycle, requestBody, authorization, digest, date);
            }
            return result;
        }

        async Task<IActionResult> ProcessLifecycleIfVerified(string lifecycleType, string requestBody, string authorizationHeader, string digestHeader, string dateHeader)
        {
            IActionResult result;
            bool requestIsVerified;
            if (environment.IsDevelopment())
            {
                requestIsVerified = true;
            }
            else
            {
                string method = HttpContext.Request.Method.ToLower();
                string route = HttpContext.Request.Path.Value;
                string signingString = $"(request-target): {method} {route}\ndigest: {digestHeader}\ndate: {dateHeader}";
                string myKeyId = authorizationHeader.Substring(17, 60);
                string maskedPubKey = configuration["PublicKey"];
                string pubKey = maskedPubKey.Replace("*", "\r\n");
                string mySignature = authorizationHeader.Substring(90, 344);

                requestIsVerified = await nodeServices.InvokeAsync<bool>("./Node/VerifyProxy.js", myKeyId, mySignature, signingString, pubKey);
            }
            
            if (requestIsVerified)
            {
                logger.LogInformation("Hello from New-SmartApp! Received verified lifecycle request.");
                result = await ProcessVerifiedLifecycle(lifecycleType, requestBody);
            }
            else
            {
                logger.LogWarning("Request was not authorized.");
                result = Unauthorized();
            }

            return result;
        }

        async Task<IActionResult> ProcessVerifiedLifecycle(string lifecycleType, string requestBody)
        {
            IActionResult result;
            switch (lifecycleType)
            {
                case "CONFIGURATION":
                    result = ProcessConfigurationLifecycle(requestBody);
                    break;
                case "INSTALL":
                    result = await ProcessInstallLifecycleAsync(requestBody);
                    break;
                case "UPDATE":
                    result = ProcessUpdateLifecycle(requestBody);
                    break;
                case "EVENT":
                    result = ProcessEventLifecycle(requestBody);
                    break;
                case "UNINSTALL":
                    result = ProcessUninstallLifecycle(requestBody);
                    break;
                default:
                    result = NotFound();
                    break;
            }
            return result;
        }

        IActionResult ProcessUninstallLifecycle(string requestBody)
        {
            UninstallLifecycle uninstallLifecycle = JsonConvert.DeserializeObject<UninstallLifecycle>(requestBody);
            return Ok(new UninstallResponse());
        }

        IActionResult ProcessEventLifecycle(string requestBody)
        {
            IActionResult result = Ok(new EventResponse());
            EventLifecycle eventLifecycle = JsonConvert.DeserializeObject<EventLifecycle>(requestBody);

            switch (eventLifecycle.EventData.Events[0].EventType)
            {
                case "DEVICE_EVENT":
                    ProcessDeviceEvent(eventLifecycle.EventData.Events[0].DeviceEvent);
                    break;
                case "TIMER_EVENT":
                    ProcessTimerEvent(eventLifecycle.EventData.Events[0].TimerEvent);
                    break;
                default:
                    result = NotFound();
                    break;
            }

            return result;
        }

        void ProcessTimerEvent(TimerEvent timerEvent)
        {
            throw new NotImplementedException();
        }

        void ProcessDeviceEvent(DeviceEvent deviceEvent)
        {
            logger.LogInformation($"Received SmartThings device event {deviceEvent.EventId} for device {deviceEvent.DeviceId} containing the following value: {deviceEvent.Value}.");
            Debug.Print(deviceEvent.Attribute);
            Debug.Print(deviceEvent.Capability);
            Debug.Print(deviceEvent.ComponentId);
            Debug.Print(deviceEvent.DeviceId);
            Debug.Print(deviceEvent.EventId);
            Debug.Print(deviceEvent.LocationId);
            Debug.Print(deviceEvent.StateChange.ToString());
            Debug.Print(deviceEvent.SubscriptionName);
            Debug.Print(deviceEvent.Value);
        }

        IActionResult ProcessUpdateLifecycle(string requestBody)
        {
            UpdateLifecycle updateLifecycle = JsonConvert.DeserializeObject<UpdateLifecycle>(requestBody);
            return Ok(new UpdateResponse());
        }

        IActionResult ProcessPingLifecycle(string requestBody)
        {
            PingLifecycle pingLifecycle = JsonConvert.DeserializeObject<PingLifecycle>(requestBody);
            return Ok(new PingResponse() { PingData = pingLifecycle.PingData });
        }

        IActionResult ProcessConfigurationLifecycle(string requestBody)
        {
            IActionResult result;
            ConfigurationLifecycle configurationLifecycle = JsonConvert.DeserializeObject<ConfigurationLifecycle>(requestBody);

            switch (configurationLifecycle.ConfigurationData.Phase)
            {
                case "INITIALIZE":
                    result = GetInitializationPhaseResponse();
                    break;
                case "PAGE":
                    result = GetPagePhaseResponse();
                    break;
                default:
                    result = NotFound();
                    break;
            }

            return result;
        }

        IActionResult GetInitializationPhaseResponse()
        {
            ConfigurationInitializationResponse response = new ConfigurationInitializationResponse()
            {
                ConfigurationData = new ConfigurationInitializationData()
                {
                    Initialize = new InitializationData()
                    {
                        Name = "SmartApp name",
                        Description = "SmartApp description",
                        Id = "MySmartApp",
                        Permissions = new List<string>() {"r:devices:*"},
                        FirstPageId = "1"
                    }
                }
            };
            return Ok(response);
        }

        IActionResult GetPagePhaseResponse()
        {
            ConfigurationPageResponse response = new ConfigurationPageResponse()
            {
                ConfigurationData = new ConfigurationPageData()
                {
                    Page = new ConfigurationPage()
                    {
                        PageId = "1",
                        Name = "My first page name",
                        NextPageId = null,
                        PreviousPageId = null,
                        Complete = true,
                        Sections = new List<PageSection>()
                        {
                            new PageSection()
                            {
                                Name = "When this opens or closes...",
                                Settings = new List<PageSetting>()
                                {
                                    new DeviceSetting()
                                    {
                                        Id = "ContactSensor",
                                        Name = "Which contact sensor?",
                                        Description = "Tap to set",
                                        Type = "DEVICE",
                                        Required = true,
                                        Multiple = false,
                                        Capabilities = new List<string>() {"contactSensor"},
                                        Permissions = new List<string>() {"r"}
                                    }
                                }
                            }
                        }
                    }
                }
            };
            return Ok(response);
        }

        async Task<IActionResult> ProcessInstallLifecycleAsync(string requestBody)
        {
            InstallLifecycle installLifecycle = JsonConvert.DeserializeObject<InstallLifecycle>(requestBody);
            DeviceConfig config = installLifecycle.InstallData.InstalledApp.Config.ContactSensor[0].DeviceConfig;
            Guid token = installLifecycle.InstallData.AuthToken;
            Guid appId = installLifecycle.InstallData.InstalledApp.InstalledAppId;
            return await CreateSubscriptionAsync(config, token, appId);
        }

        async Task<IActionResult> CreateSubscriptionAsync(DeviceConfig config, Guid token, Guid appId)
        {
            const string baseUrl = "https://api.smartthings.com";
            string path = $"/installedapps/{appId}/subscriptions";

            DeviceSubscription subscription = new DeviceSubscription
            {
                sourceType = "DEVICE",
                device = new DeviceSubscriptionInfo
                {
                    componentId = config.ComponentId,
                    deviceId = config.DeviceId.ToString(),
                    capability = "contactSensor",
                    attribute = "contact",
                    stateChangeOnly = true,
                    subscriptionName = "contact_subscription",
                    value = "open"
                }
            };

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
            HttpResponseMessage response = await client.PostAsJsonAsync(path, subscription);
            if (response.IsSuccessStatusCode)
            {
                Debug.Print("Subscription was successful");
            }
            else
            {
                Debug.Print("Subscription was not successful");
                Debug.Print(response.StatusCode.ToString());
                Debug.Print(response.ReasonPhrase);
            }
            return Ok(new InstallResponse());
        }
    }
}