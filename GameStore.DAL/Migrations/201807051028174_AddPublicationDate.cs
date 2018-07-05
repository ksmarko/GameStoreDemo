namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPublicationDate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Body = c.String(),
                        GameId = c.Int(nullable: false),
                        Parent_Id = c.Int(),
                        Publisher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.Parent_Id)
                .ForeignKey("dbo.Publishers", t => t.Publisher_Id)
                .Index(t => t.GameId)
                .Index(t => t.Parent_Id)
                .Index(t => t.Publisher_Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Views = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        PublicationDate = c.DateTime(nullable: false),
                        PublisherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.PlatformTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameGenres",
                c => new
                    {
                        Game_Id = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_Id, t.Genre_Id })
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.PlatformTypeGames",
                c => new
                    {
                        PlatformType_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlatformType_Id, t.Game_Id })
                .ForeignKey("dbo.PlatformTypes", t => t.PlatformType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.PlatformType_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Publisher_Id", "dbo.Publishers");
            DropForeignKey("dbo.Comments", "Parent_Id", "dbo.Comments");
            DropForeignKey("dbo.Games", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.PlatformTypeGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlatformTypeGames", "PlatformType_Id", "dbo.PlatformTypes");
            DropForeignKey("dbo.GameGenres", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.GameGenres", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Genres", "ParentId", "dbo.Genres");
            DropForeignKey("dbo.Comments", "GameId", "dbo.Games");
            DropIndex("dbo.PlatformTypeGames", new[] { "Game_Id" });
            DropIndex("dbo.PlatformTypeGames", new[] { "PlatformType_Id" });
            DropIndex("dbo.GameGenres", new[] { "Genre_Id" });
            DropIndex("dbo.GameGenres", new[] { "Game_Id" });
            DropIndex("dbo.Genres", new[] { "ParentId" });
            DropIndex("dbo.Games", new[] { "PublisherId" });
            DropIndex("dbo.Comments", new[] { "Publisher_Id" });
            DropIndex("dbo.Comments", new[] { "Parent_Id" });
            DropIndex("dbo.Comments", new[] { "GameId" });
            DropTable("dbo.PlatformTypeGames");
            DropTable("dbo.GameGenres");
            DropTable("dbo.Publishers");
            DropTable("dbo.PlatformTypes");
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
            DropTable("dbo.Comments");
        }
    }
}
