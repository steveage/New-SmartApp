using System.Collections.Generic;

namespace NewSmartApp.Webservice.Models
{
    public class ConfigurationPageConfig
    {
        public IList<object> MySmartApp { get; set; }
        public IList<ConfigDevice> ContactSensor { get; set; }
    }
}