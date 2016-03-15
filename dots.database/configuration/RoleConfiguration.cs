namespace dots.database.configuration
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using models;

    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            var index = 0;

            HasKey(r => r.RecordId);
            Property(r => r.RecordId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnOrder(++index);

            Property(u => u.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(r => r.ModifiedBy)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(r => r.ModifiedOn)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);

            Property(r => r.CreatedBy)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(r => r.CreatedOn)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);
        }
    }
}
