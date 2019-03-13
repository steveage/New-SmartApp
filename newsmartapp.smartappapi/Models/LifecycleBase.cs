using System;

namespace NewSmartApp.Webservice.Models
{
    public class LifecycleBase
    {
        public string Lifecycle { get; set; }
        public Guid ExecutionId { get; set; }
        public string Locale { get; set; }
        public string Version { get; set; }
    }
}