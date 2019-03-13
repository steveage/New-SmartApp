namespace NewSmartApp.Webservice.Models
{
    public class DeviceEvent
    {
        public string SubscriptionName { get; set; }
        public string EventId { get; set; }
        public string LocationId { get; set; }
        public string DeviceId { get; set; }
        public string ComponentId { get; set; }
        public string Capability { get; set; }
        public string Attribute { get; set; }
        public string Value { get; set; }
        public bool StateChange { get; set; }
    }
}