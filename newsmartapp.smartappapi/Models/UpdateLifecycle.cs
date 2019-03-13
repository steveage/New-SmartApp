namespace NewSmartApp.Webservice.Models
{
    public class UpdateLifecycle : ClientLifecycle
    {
        public UpdateData UpdateData { get; set; }
        public InstallSettings Settings { get; set; }
    }
}