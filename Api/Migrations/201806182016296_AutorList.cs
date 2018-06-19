namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutorList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Livro", "AutorId", "dbo.Autor");
            DropIndex("dbo.Livro", new[] { "AutorId" });
            AddColumn("dbo.Autor", "Livro_LivroId", c => c.Int());
            AddColumn("dbo.Livro", "Autor_AutorId", c => c.Int());
            AddColumn("dbo.Livro", "Autor_AutorId1", c => c.Int());
            CreateIndex("dbo.Autor", "Livro_LivroId");
            CreateIndex("dbo.Livro", "Autor_AutorId");
            CreateIndex("dbo.Livro", "Autor_AutorId1");
            AddForeignKey("dbo.Autor", "Livro_LivroId", "dbo.Livro", "LivroId");
            AddForeignKey("dbo.Livro", "Autor_AutorId1", "dbo.Autor", "AutorId");
            AddForeignKey("dbo.Livro", "Autor_AutorId", "dbo.Autor", "AutorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Livro", "Autor_AutorId", "dbo.Autor");
            DropForeignKey("dbo.Livro", "Autor_AutorId1", "dbo.Autor");
            DropForeignKey("dbo.Autor", "Livro_LivroId", "dbo.Livro");
            DropIndex("dbo.Livro", new[] { "Autor_AutorId1" });
            DropIndex("dbo.Livro", new[] { "Autor_AutorId" });
            DropIndex("dbo.Autor", new[] { "Livro_LivroId" });
            DropColumn("dbo.Livro", "Autor_AutorId1");
            DropColumn("dbo.Livro", "Autor_AutorId");
            DropColumn("dbo.Autor", "Livro_LivroId");
            CreateIndex("dbo.Livro", "AutorId");
            AddForeignKey("dbo.Livro", "AutorId", "dbo.Autor", "AutorId", cascadeDelete: true);
        }
    }
}
