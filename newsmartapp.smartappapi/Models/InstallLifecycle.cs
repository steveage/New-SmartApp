namespace NewSmartApp.Webservice.Models
{
    public class InstallLifecycle : ClientLifecycle
    {
        public InstallData InstallData { get; set; }
        public InstallSettings Settings { get; set; }
    }
}