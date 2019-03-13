namespace NewSmartApp.Webservice.Models
{
    public class ConfigurationLifecycle : ClientLifecycle
    {
        public ConfigurationData ConfigurationData { get; set; }
        public object Settings { get; set; }
    }
}