namespace NewSmartApp.Webservice.Models
{
    public class ConfigurationData
    {
        public string InstalledAppId { get; set; }
        public string Phase { get; set; }
        public string PageId { get; set; }
        public string PreviousPageId { get; set; }
        public ConfigurationPageConfig Config { get; set; }
    }
}