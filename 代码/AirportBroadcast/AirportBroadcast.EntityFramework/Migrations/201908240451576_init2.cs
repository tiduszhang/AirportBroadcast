namespace AirportBroadcast.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlayAudioLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlayAudioLogs");
        }
    }
}
