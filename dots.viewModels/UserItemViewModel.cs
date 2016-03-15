namespace dots.viewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserItemViewModel
    {
        public long RecordId { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Is Administrator")]
        public bool IsAdministrator { get; set; }

        [Display(Name = "Is Editor")]
        public bool IsEditor { get; set; }

        [Display(Name = "Is Updater")]
        public bool IsUpdater { get; set; }
    }
}
