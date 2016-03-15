namespace dots.database.configuration
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using models;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            var index = 0;

            HasKey(u => u.RecordId);
            Property(u => u.RecordId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnOrder(++index);

            Property(u => u.Username)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(u => u.FirstName)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(u => u.LastName)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(u => u.ModifiedBy)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(u => u.ModifiedOn)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);

            Property(u => u.CreatedBy)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(u => u.CreatedOn)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);
        }
    }
}
