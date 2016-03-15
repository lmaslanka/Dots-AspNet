namespace dots.database
{
    using configuration;
    using models;
    using System.Data.Entity;

    public class DotsContext : DbContext
    {
        public DbSet<Outbreak> Outbreaks { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityType> FacilityTypes { get; set; }
        public DbSet<OutbreakLocation> OutbreakLocations { get; set; }
        public DbSet<Pathogen> Pathogens { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DotsContext() : base("DotsConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CountyConfiguration());
            modelBuilder.Configurations.Add(new FacilityConfiguration());
            modelBuilder.Configurations.Add(new FacilityTypeConfiguration());
            modelBuilder.Configurations.Add(new OutbreakConfiguration());
            modelBuilder.Configurations.Add(new OutbreakLocationConfiguration());
            modelBuilder.Configurations.Add(new PathogenConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
        }
    }
}