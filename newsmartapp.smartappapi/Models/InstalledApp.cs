using System;
using System.Collections.Generic;

namespace NewSmartApp.Webservice.Models
{
    public class InstalledApp
    {
        public Guid InstalledAppId { get; set; }
        public Guid LocationId { get; set; }
        public InstallConfig Config { get; set; }
        public IList<string> Permissions { get; set; }
    }
}