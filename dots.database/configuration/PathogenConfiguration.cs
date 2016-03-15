namespace dots.database.configuration
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using models;

    public class PathogenConfiguration : EntityTypeConfiguration<Pathogen>
    {
        public PathogenConfiguration()
        {
            var index = 0;

            HasKey(d => d.RecordId);
            Property(d => d.RecordId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnOrder(++index);

            Property(evt => evt.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(evt => evt.ModifiedBy)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(evt => evt.ModifiedOn)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);

            Property(evt => evt.CreatedBy)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(evt => evt.CreatedOn)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);
        }
    }
}
