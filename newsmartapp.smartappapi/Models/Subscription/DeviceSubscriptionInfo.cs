namespace NewSmartApp.Webservice.Models.Subscription
{
    public class DeviceSubscriptionInfo
    {
        public string componentId { get; set; }
        public string deviceId { get; set; }
        public string capability { get; set; }
        public string attribute { get; set; }
        public bool stateChangeOnly { get; set; }
        public string subscriptionName { get; set; }
        public string value { get; set; }
    }
}