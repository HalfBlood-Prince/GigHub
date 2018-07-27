namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatingGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (1, N'Jazz')
                INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (2, N'Blues')
                INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (3, N'Country')
                INSERT INTO [dbo].[Genres] ([Id], [Name]) VALUES (4, N'Rock')");
        }
        
        public override void Down()
        {
            Sql("DELTE FROM Genres WHERE Id IN (1,2,3,4)");
        }
    }
}
