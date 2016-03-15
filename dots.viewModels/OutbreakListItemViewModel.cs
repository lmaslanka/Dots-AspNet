namespace dots.viewModels
{
    using System.Collections.Generic;

    public class OutbreakListItemViewModel
    {
        public string County { get; set; }
        public List<OutbreakItemViewModel> Outbreaks { get; set; }
    }
}
