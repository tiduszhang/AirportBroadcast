namespace AirportBroadcast.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AudioPlaySets", "TopPwrPort_Id", "dbo.TopPwrPorts");
            DropForeignKey("dbo.AudioPlaySets", "TopPwrPort_Id1", "dbo.TopPwrPorts");
            DropForeignKey("dbo.TopPwrPorts", "AudioPlaySet_Id", "dbo.AudioPlaySets");
            DropForeignKey("dbo.TopPwrPorts", "AudioPlaySet_Id1", "dbo.AudioPlaySets");
            DropIndex("dbo.AudioPlaySets", new[] { "TopPwrPort_Id" });
            DropIndex("dbo.AudioPlaySets", new[] { "TopPwrPort_Id1" });
            DropIndex("dbo.TopPwrPorts", new[] { "AudioPlaySet_Id" });
            DropIndex("dbo.TopPwrPorts", new[] { "AudioPlaySet_Id1" });
            CreateTable(
                "dbo.AudioPlaySetTopPwrPorts",
                c => new
                    {
                        AudioPlaySet_Id = c.Int(nullable: false),
                        TopPwrPort_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AudioPlaySet_Id, t.TopPwrPort_Id })
                .ForeignKey("dbo.AudioPlaySets", t => t.AudioPlaySet_Id, cascadeDelete: true)
                .ForeignKey("dbo.TopPwrPorts", t => t.TopPwrPort_Id, cascadeDelete: true)
                .Index(t => t.AudioPlaySet_Id)
                .Index(t => t.TopPwrPort_Id);
            
            CreateTable(
                "dbo.AudioPlaySetTopPwrPort1",
                c => new
                    {
                        AudioPlaySet_Id = c.Int(nullable: false),
                        TopPwrPort_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AudioPlaySet_Id, t.TopPwrPort_Id })
                .ForeignKey("dbo.AudioPlaySets", t => t.AudioPlaySet_Id, cascadeDelete: true)
                .ForeignKey("dbo.TopPwrPorts", t => t.TopPwrPort_Id, cascadeDelete: true)
                .Index(t => t.AudioPlaySet_Id)
                .Index(t => t.TopPwrPort_Id);
            
            DropColumn("dbo.AudioPlaySets", "TopPwrPort_Id");
            DropColumn("dbo.AudioPlaySets", "TopPwrPort_Id1");
            DropColumn("dbo.TopPwrPorts", "AudioPlaySet_Id");
            DropColumn("dbo.TopPwrPorts", "AudioPlaySet_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TopPwrPorts", "AudioPlaySet_Id1", c => c.Int());
            AddColumn("dbo.TopPwrPorts", "AudioPlaySet_Id", c => c.Int());
            AddColumn("dbo.AudioPlaySets", "TopPwrPort_Id1", c => c.Int());
            AddColumn("dbo.AudioPlaySets", "TopPwrPort_Id", c => c.Int());
            DropForeignKey("dbo.AudioPlaySetTopPwrPort1", "TopPwrPort_Id", "dbo.TopPwrPorts");
            DropForeignKey("dbo.AudioPlaySetTopPwrPort1", "AudioPlaySet_Id", "dbo.AudioPlaySets");
            DropForeignKey("dbo.AudioPlaySetTopPwrPorts", "TopPwrPort_Id", "dbo.TopPwrPorts");
            DropForeignKey("dbo.AudioPlaySetTopPwrPorts", "AudioPlaySet_Id", "dbo.AudioPlaySets");
            DropIndex("dbo.AudioPlaySetTopPwrPort1", new[] { "TopPwrPort_Id" });
            DropIndex("dbo.AudioPlaySetTopPwrPort1", new[] { "AudioPlaySet_Id" });
            DropIndex("dbo.AudioPlaySetTopPwrPorts", new[] { "TopPwrPort_Id" });
            DropIndex("dbo.AudioPlaySetTopPwrPorts", new[] { "AudioPlaySet_Id" });
            DropTable("dbo.AudioPlaySetTopPwrPort1");
            DropTable("dbo.AudioPlaySetTopPwrPorts");
            CreateIndex("dbo.TopPwrPorts", "AudioPlaySet_Id1");
            CreateIndex("dbo.TopPwrPorts", "AudioPlaySet_Id");
            CreateIndex("dbo.AudioPlaySets", "TopPwrPort_Id1");
            CreateIndex("dbo.AudioPlaySets", "TopPwrPort_Id");
            AddForeignKey("dbo.TopPwrPorts", "AudioPlaySet_Id1", "dbo.AudioPlaySets", "Id");
            AddForeignKey("dbo.TopPwrPorts", "AudioPlaySet_Id", "dbo.AudioPlaySets", "Id");
            AddForeignKey("dbo.AudioPlaySets", "TopPwrPort_Id1", "dbo.TopPwrPorts", "Id");
            AddForeignKey("dbo.AudioPlaySets", "TopPwrPort_Id", "dbo.TopPwrPorts", "Id");
        }
    }
}
