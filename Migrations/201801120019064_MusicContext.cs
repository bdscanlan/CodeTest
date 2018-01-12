namespace Validus_Code_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MusicContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        name = c.String(),
                        yearReleased = c.Long(nullable: false),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        artist_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artist", t => t.artist_Id)
                .Index(t => t.artist_Id);
            
            CreateTable(
                "dbo.Artist",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        name = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Song",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        name = c.String(),
                        track = c.Int(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        album_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Album", t => t.album_Id)
                .Index(t => t.album_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Song", "album_Id", "dbo.Album");
            DropForeignKey("dbo.Album", "artist_Id", "dbo.Artist");
            DropIndex("dbo.Song", new[] { "album_Id" });
            DropIndex("dbo.Album", new[] { "artist_Id" });
            DropTable("dbo.Song");
            DropTable("dbo.Artist");
            DropTable("dbo.Album");
        }
    }
}
