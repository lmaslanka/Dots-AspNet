namespace dots.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Counties",
                c => new
                    {
                        RecordId = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 80),
                        ModifiedBy = c.String(maxLength: 80),
                        ModifiedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 80),
                        CreatedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Timestamp = c.Binary(),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        RecordId = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 80),
                        ModifiedBy = c.String(maxLength: 80),
                        ModifiedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 80),
                        CreatedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Timestamp = c.Binary(),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateTable(
                "dbo.FacilityTypes",
                c => new
                    {
                        RecordId = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 80),
                        ModifiedBy = c.String(maxLength: 80),
                        ModifiedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 80),
                        CreatedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Timestamp = c.Binary(),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateTable(
                "dbo.OutbreakLocations",
                c => new
                    {
                        RecordId = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 80),
                        ModifiedBy = c.String(maxLength: 80),
                        ModifiedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 80),
                        CreatedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Timestamp = c.Binary(),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateTable(
                "dbo.Outbreaks",
                c => new
                    {
                        RecordId = c.Long(nullable: false, identity: true),
                        FacilityTypeId = c.Long(nullable: false),
                        CountyId = c.Long(nullable: false),
                        Facility = c.String(maxLength: 80),
                        OutbreakLocation = c.String(maxLength: 80),
                        IsOutbreakDeclared = c.Boolean(nullable: false),
                        IsOutbreakDeclaredOver = c.Boolean(nullable: false),
                        OutbreakDeclaredDate = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        OutbreakDeclaredOverDate = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        IsAdmissionsClosed = c.Boolean(nullable: false),
                        IsAdmissionsOpened = c.Boolean(nullable: false),
                        AdmissionsCloseDate = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        AdmissionsOpenDate = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Pathogen = c.String(maxLength: 80),
                        ModifiedBy = c.String(maxLength: 80),
                        ModifiedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 80),
                        CreatedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Timestamp = c.Binary(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Counties", t => t.CountyId)
                .ForeignKey("dbo.FacilityTypes", t => t.FacilityTypeId)
                .Index(t => t.FacilityTypeId)
                .Index(t => t.CountyId);
            
            CreateTable(
                "dbo.Pathogens",
                c => new
                    {
                        RecordId = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 80),
                        ModifiedBy = c.String(maxLength: 80),
                        ModifiedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 80),
                        CreatedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Timestamp = c.Binary(),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RecordId = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 80),
                        ModifiedBy = c.String(maxLength: 80),
                        ModifiedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 80),
                        CreatedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Timestamp = c.Binary(),
                    })
                .PrimaryKey(t => t.RecordId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RecordId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                        ModifiedBy = c.String(maxLength: 80),
                        ModifiedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 80),
                        CreatedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Timestamp = c.Binary(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        RecordId = c.Long(nullable: false, identity: true),
                        Username = c.String(maxLength: 80),
                        FirstName = c.String(maxLength: 80),
                        LastName = c.String(maxLength: 80),
                        ModifiedBy = c.String(maxLength: 80),
                        ModifiedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 80),
                        CreatedOn = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Timestamp = c.Binary(),
                    })
                .PrimaryKey(t => t.RecordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Outbreaks", "FacilityTypeId", "dbo.FacilityTypes");
            DropForeignKey("dbo.Outbreaks", "CountyId", "dbo.Counties");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Outbreaks", new[] { "CountyId" });
            DropIndex("dbo.Outbreaks", new[] { "FacilityTypeId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Pathogens");
            DropTable("dbo.Outbreaks");
            DropTable("dbo.OutbreakLocations");
            DropTable("dbo.FacilityTypes");
            DropTable("dbo.Facilities");
            DropTable("dbo.Counties");
        }
    }
}
