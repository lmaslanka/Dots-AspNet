namespace dots.models
{
    using System;

    public class Outbreak
    {
        public long RecordId { get; set; }
        public long FacilityTypeId { get; set; }
        public FacilityType FacilityType { get; set; }
        public long CountyId { get; set; }
        public County County { get; set; }
        public string Facility { get; set; }
        public string OutbreakLocation { get; set; }
        public bool IsOutbreakDeclared { get; set; }
        public bool IsOutbreakDeclaredOver { get; set; }
        public DateTime OutbreakDeclaredDate { get; set; }
        public DateTime OutbreakDeclaredOverDate { get; set; }
        public bool IsAdmissionsClosed { get; set; }
        public bool IsAdmissionsOpened { get; set; }
        public DateTime AdmissionsCloseDate { get; set; }
        public DateTime AdmissionsOpenDate { get; set; }
        public string Pathogen { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
