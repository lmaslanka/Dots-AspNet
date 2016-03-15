namespace dots.viewModels
{
    public class OutbreakItemViewModel
    {
        public long RecordId { get; set; }
        public string Facility { get; set; }
        public string OutbreakLocation { get; set; }
        public string FacilityType { get; set; }
        public string OutbreakDeclaredDate { get; set; }
        public string OutbreakDeclaredOverDate { get; set; }
        public bool IsOutbreakDeclared { get; set; }
        public bool IsOutbreakDeclaredOver { get; set; }
        public string AdmissionsOpenDate { get; set; }
        public string AdmissionsCloseDate { get; set; }
        public bool IsAdmissionsClosed { get; set; }
        public bool IsAdmissionsOpened { get; set; }
        public string Pathogen { get; set; }
        public string County { get; set; }
        public string OutbreakLevel { get; set; }
        public int OutbreakDurationInDays { get; set; }
    }
}
