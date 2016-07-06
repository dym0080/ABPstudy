namespace ABPDemoNoZero.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddictionary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dictionary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dictionary");
        }
    }
}
