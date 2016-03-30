namespace dots.database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_v1_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Outbreaks", "Comment", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Outbreaks", "Comment");
        }
    }
}
