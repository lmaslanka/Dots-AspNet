namespace dots.viewModels
{
    public class UserItemViewModel
    {
        public long RecordId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsEditor { get; set; }
        public bool IsUpdater { get; set; }
    }
}
