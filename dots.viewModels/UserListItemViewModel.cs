namespace dots.viewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserListItemViewModel
    {
        [Display(Name = "Record Id")]
        public long RecordId { get; set; }
        public string Username { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
