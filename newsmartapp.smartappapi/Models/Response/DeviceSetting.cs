using System.Collections.Generic;

namespace NewSmartApp.Webservice.Models.Response
{
    public class DeviceSetting : PageSetting
    {
        public bool Required { get; set; }
        public bool Multiple { get; set; }
        public IList<string> Capabilities { get; set; }
        public IList<string> Permissions { get; set; }
    }
}