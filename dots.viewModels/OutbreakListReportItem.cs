namespace dots.viewModels
{
    using System.Collections.Generic;

    public class OutbreakListReportItem
    {
        public string County { get; set; }
        public List<OutbreakReportItem> Outbreaks { get; set; }
    }
}
