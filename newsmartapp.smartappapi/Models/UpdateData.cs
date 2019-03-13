using System.Collections.Generic;

namespace NewSmartApp.Webservice.Models
{
    public class UpdateData : InstallData
    {
        public InstallConfig PreviousConfig { get; set; }
        public IList<string> PreviousPermissions { get; set; }
    }
}