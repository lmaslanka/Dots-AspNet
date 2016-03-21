namespace dots.viewModels
{
    using System.ComponentModel.DataAnnotations;

    public class FacilityItemViewModel
    {
        [Display(Name = "Record Id")]
        public long RecordId { get; set; }

        [Display(Name = "Facility Name")]
        [Required]
        [StringLength(80, MinimumLength = 6)]
        public string Name { get; set; }
    }
}