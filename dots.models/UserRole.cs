namespace dots.models
{
    using System;

    public class UserRole
    {
        public long RecordId { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
