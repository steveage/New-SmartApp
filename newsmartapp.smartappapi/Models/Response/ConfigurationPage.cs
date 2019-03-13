using System.Collections.Generic;

namespace NewSmartApp.Webservice.Models.Response
{
    public class ConfigurationPage
    {
        public string PageId { get; set; }
        public string Name { get; set; }
        public string NextPageId { get; set; }
        public string PreviousPageId { get; set; }
        public bool Complete { get; set; }
        public IList<PageSection> Sections { get; set; }
    }
}
