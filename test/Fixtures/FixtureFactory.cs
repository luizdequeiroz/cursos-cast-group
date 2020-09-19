using domain;
using domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace test.Fixtures
{
    public static class FixtureFactory
    {
        public static CursosCastGroupContext GetContext() =>
            new CursosCastGroupContext(
                new DbContextOptionsBuilder<CursosCastGroupContext>()
                    .UseInMemoryDatabase($"castgroup{new Random().Next()}")
                    .Options
            );

        public static CursosCastGroupContext ApplyCursoMocks(this CursosCastGroupContext context)
        {
            context.Cursos.AddRange(
                new Curso
                {
                    Assunto = "Javascript",
                    DataInicio = new DateTime(2020, 10, 01),
                    DataTermino = new DateTime(2021, 01, 30),
                    QuantidadeAlunos = 15,
                    CodigoCategoria = 2,
                    RegisterDate = DateTime.Now
                },
                new Curso
                {
                    Assunto = "SQL",
                    DataInicio = new DateTime(2021, 10, 01),
                    DataTermino = new DateTime(2022, 01, 30),
                    QuantidadeAlunos = 20,
                    CodigoCategoria = 2,
                    RegisterDate = DateTime.Now
                }
            );
            context.SaveChanges();

            return context;
        }
    }
}
