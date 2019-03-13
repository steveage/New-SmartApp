using System.Collections.Generic;

namespace NewSmartApp.Webservice.Models
{
    public class EventData
    {
        public string AuthToken { get; set; }
        public InstalledApp InstalledApp { get; set; }
        public IList<AppEvent> Events { get; set; }
    }
}
