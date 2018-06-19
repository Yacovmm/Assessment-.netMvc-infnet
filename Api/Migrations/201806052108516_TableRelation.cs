namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LivroAutor", "Livro_LivroId", "dbo.Livro");
            DropForeignKey("dbo.LivroAutor", "Autor_AutorId", "dbo.Autor");
            DropIndex("dbo.LivroAutor", new[] { "Livro_LivroId" });
            DropIndex("dbo.LivroAutor", new[] { "Autor_AutorId" });
            AddColumn("dbo.Livro", "AutorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Livro", "AutorId");
            AddForeignKey("dbo.Livro", "AutorId", "dbo.Autor", "AutorId", cascadeDelete: true);
            DropTable("dbo.LivroAutor");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LivroAutor",
                c => new
                    {
                        Livro_LivroId = c.Int(nullable: false),
                        Autor_AutorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Livro_LivroId, t.Autor_AutorId });
            
            DropForeignKey("dbo.Livro", "AutorId", "dbo.Autor");
            DropIndex("dbo.Livro", new[] { "AutorId" });
            DropColumn("dbo.Livro", "AutorId");
            CreateIndex("dbo.LivroAutor", "Autor_AutorId");
            CreateIndex("dbo.LivroAutor", "Livro_LivroId");
            AddForeignKey("dbo.LivroAutor", "Autor_AutorId", "dbo.Autor", "AutorId", cascadeDelete: true);
            AddForeignKey("dbo.LivroAutor", "Livro_LivroId", "dbo.Livro", "LivroId", cascadeDelete: true);
        }
    }
}
