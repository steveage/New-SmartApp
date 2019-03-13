using System;

namespace NewSmartApp.Webservice.Models
{
    public class InstallData
    {
        public Guid AuthToken { get; set; }
        public Guid RefreshToken { get; set; }
        public InstalledApp InstalledApp { get; set; }
    }
}