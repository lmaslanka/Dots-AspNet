namespace dots.database.configuration
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using models;

    public class OutbreakConfiguration : EntityTypeConfiguration<Outbreak>
    {
        public OutbreakConfiguration()
        {
            var index = 0;

            HasKey(d => d.RecordId);
            Property(d => d.RecordId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnOrder(++index);

            Property(o => o.FacilityTypeId)
                .HasColumnOrder(++index);

            Property(o => o.CountyId)
                .HasColumnOrder(++index);

            Property(evt => evt.Facility)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(evt => evt.OutbreakLocation)
                .HasColumnType("nvarchar")
                .HasMaxLength(80)
                .HasColumnOrder(++index);

            Property(evt => evt.IsOutbreakDeclared)
                .HasColumnOrder(++index);

            Property(evt => evt.IsOutbreakDeclaredOver)
                .HasColumnOrder(++index);

            Property(evt => evt.OutbreakDeclaredDate)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);

            Property(evt => evt.OutbreakDeclaredOverDate)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);

            Property(evt => evt.IsAdmissionsClosed)
                .HasColumnOrder(++index);

            Property(evt => evt.IsAdmissionsOpened)
                .HasColumnOrder(++index);

            Property(evt => evt.AdmissionsCloseDate)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);

            Property(evt => evt.AdmissionsOpenDate)
                .HasColumnType("datetime2")
                .HasPrecision(0)
                .HasColumnOrder(++index);

            Property(evt => evt.Pathogen)
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

            HasRequired(o => o.FacilityType)
                .WithMany()
                .HasForeignKey(o => o.FacilityTypeId)
                .WillCascadeOnDelete(false);

            HasRequired(o => o.County)
                .WithMany()
                .HasForeignKey(o => o.CountyId)
                .WillCascadeOnDelete(false);
        }
    }
}