using domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace domain
{
    public class CursosCastGroupContext : DbContext
    {
        public CursosCastGroupContext(DbContextOptions<CursosCastGroupContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria
                {
                    Codigo = 1,
                    Descricao = "Comportamental",
                    RegisterDate = DateTime.Now,
                    UpdateDate = null
                },
                new Categoria
                {
                    Codigo = 2,
                    Descricao = "Programação",
                    RegisterDate = DateTime.Now,
                    UpdateDate = null
                },
                new Categoria
                {
                    Codigo = 3,
                    Descricao = "Qualidade",
                    RegisterDate = DateTime.Now,
                    UpdateDate = null
                },
                new Categoria
                {
                    Codigo = 4,
                    Descricao = "Processos",
                    RegisterDate = DateTime.Now,
                    UpdateDate = null
                }
            );
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
