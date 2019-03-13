namespace NewSmartApp.Webservice.Models
{
    public class AppEvent
    {
        public string EventType { get; set; }
        public DeviceEvent DeviceEvent { get; set; }
        public TimerEvent TimerEvent { get; set; }
    }
}