namespace SNAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class af : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dictionary", "LastModificationTime", c => c.DateTime(precision: 0));
            AddColumn("dbo.Dictionary", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.Dictionary", "CreationTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Dictionary", "CreatorUserId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dictionary", "CreatorUserId");
            DropColumn("dbo.Dictionary", "CreationTime");
            DropColumn("dbo.Dictionary", "LastModifierUserId");
            DropColumn("dbo.Dictionary", "LastModificationTime");
        }
    }
}
