namespace AirportBroadcast.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AudioPlaySets", "TopPwrPort_Id1", c => c.Int());
            CreateIndex("dbo.AudioPlaySets", "TopPwrPort_Id1");
            AddForeignKey("dbo.AudioPlaySets", "TopPwrPort_Id1", "dbo.TopPwrPorts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AudioPlaySets", "TopPwrPort_Id1", "dbo.TopPwrPorts");
            DropIndex("dbo.AudioPlaySets", new[] { "TopPwrPort_Id1" });
            DropColumn("dbo.AudioPlaySets", "TopPwrPort_Id1");
        }
    }
}
