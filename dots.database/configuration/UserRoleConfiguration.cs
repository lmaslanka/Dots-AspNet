namespace dots.database.configuration
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using models;

    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            var index = 0;

            HasKey(ur => ur.RecordId);
            Property(ur => ur.RecordId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnOrder(++index);

            Property(ur => ur.UserId)
                .HasColumnOrder(++index);

            Property(ur => ur.RoleId)
                .HasColumnOrder(++index);

            Property(ur => ur.ModifiedBy)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(ur => ur.ModifiedOn)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);

            Property(ur => ur.CreatedBy)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(ur => ur.CreatedOn)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);

            HasRequired(ur => ur.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .WillCascadeOnDelete(false);

            HasRequired(ur => ur.Role)
                .WithMany()
                .HasForeignKey(o => o.RoleId)
                .WillCascadeOnDelete(false);
        }
    }
}
