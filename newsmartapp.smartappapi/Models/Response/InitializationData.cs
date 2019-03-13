using System.Collections.Generic;

namespace NewSmartApp.Webservice.Models.Response
{
    public class InitializationData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public IList<string> Permissions { get; set; }
        public string FirstPageId { get; set; }
    }
}
