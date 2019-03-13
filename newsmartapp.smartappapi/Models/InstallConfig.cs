using System.Collections.Generic;

namespace NewSmartApp.Webservice.Models
{
    public class InstallConfig
    {
        public IList<ConfigDevice> ContactSensor { get; set; }
        public IList<object> MySmartApp { get; set; }
    }
}