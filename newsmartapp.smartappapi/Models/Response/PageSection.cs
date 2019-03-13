using System.Collections.Generic;

namespace NewSmartApp.Webservice.Models.Response
{
    public class PageSection
    {
        public string Name { get; set; }
        public IList<PageSetting> Settings { get; set; }
    }
}
