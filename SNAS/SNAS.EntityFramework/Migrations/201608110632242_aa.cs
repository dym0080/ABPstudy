namespace SNAS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Dictionary");
            AlterColumn("dbo.Dictionary", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Dictionary", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Dictionary");
            AlterColumn("dbo.Dictionary", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Dictionary", "Id");
        }
    }
}
