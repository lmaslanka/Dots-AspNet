namespace dots.viewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class OutbreakItemEditViewModel
    {
        public long RecordId { get; set; }

        [Display(Name = "Facility Name")]
        public string Facility { get; set; }
        public List<FacilityItemViewModel> Facilities { get; set; }

        [Display(Name = "Outbreak Location")]
        public string OutbreakLocation { get; set; }
        public List<OutbreakLocationItemViewModel> OutbreakLocations { get; set; }

        [Display(Name = "Facility Type")]
        public long FacilityTypeId { get; set; }
        public List<FacilityTypeItemViewModel> FacilityTypes { get; set; }

        [Display(Name = "Outbreak Declared Date")]
        public DateTime OutbreakDeclaredDate { get; set; }

        [Display(Name = "Outbreak Declared Over Date")]
        public DateTime OutbreakDeclaredOverDate { get; set; }

        public bool IsOutbreakDeclared { get; set; }

        [Display(Name = "Outbreak Declared Over")]
        public bool IsOutbreakDeclaredOver { get; set; }

        [Display(Name = "Admissions Reopened Date")]
        public DateTime AdmissionsOpenDate { get; set; }

        [Display(Name = "Admissions Close Date")]
        public DateTime AdmissionsCloseDate { get; set; }

        [Display(Name = "Is Admitting Closed")]
        public bool IsAdmissionsClosed { get; set; }

        [Display(Name = "Is Admitting Reopened")]
        public bool IsAdmissionsOpened { get; set; }

        [Display(Name = "Pathogen")]
        public string Pathogen { get; set; }
        public List<PathogenItemViewModel> Pathogens { get; set; }
        
        [Display(Name = "County")]
        public long CountyId { get; set; }
        public List<CountyItemViewModel> Counties { get; set; }
    }
}