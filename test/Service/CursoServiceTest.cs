using domain.Entities;
using domain.Enums;
using repository.Implementations;
using service;
using service.Implementations;
using System;
using System.Threading.Tasks;
using test.Fixtures;
using Xunit;

namespace test.Service
{
    public class CursoServiceTest
    {
        private readonly ICursoService cursoService;

        public CursoServiceTest()
        {
            cursoService = new CursoService(
                new GenericRepository<Curso>(
                    FixtureFactory.GetContext().ApplyCursoMocks()
                )
            );
        }

        [Fact]
        public async Task SetNew_QuandoPassarPeriodoJaOcupado_DeveRetornarCodigoDeErroAsync()
        {
            var curso = new Curso
            {
                Assunto = "CSharp",
                DataInicio = new DateTime(2020, 10, 01),
                DataTermino = new DateTime(2021, 01, 30),
                QuantidadeAlunos = 10,
                CodigoCategoria = 2
            };

            var cursoInserted = await cursoService.SetNewAsync(curso);

            Assert.Equal(Code.BUSY_PERIOD, cursoInserted.code);
        }

        [Fact]
        public async Task Alter_QuandoPassarPeriodoJaOcupado_DeveRetornarCodigoDeErroAsync()
        {
            var curso = new Curso
            {
                Id = 2,
                DataInicio = new DateTime(2020, 10, 01),
                DataTermino = new DateTime(2021, 01, 30)
            };

            var cursoAltered = await cursoService.AlterAsync(curso);

            Assert.Equal(Code.BUSY_PERIOD, cursoAltered.code);
        }

        [Fact]
        public async Task SetNew_QuandoPassarDataInicioMaiorQueDataTermino_DeveRetornarCodigoDeErroAsync()
        {
            var curso = new Curso
            {
                Assunto = "CSharp",
                DataInicio = new DateTime(2021, 01, 30),                
                DataTermino = new DateTime(2020, 10, 01),
                QuantidadeAlunos = 10,
                CodigoCategoria = 2
            };

            var cursoInserted = await cursoService.SetNewAsync(curso);

            Assert.Equal(Code.INVALID_DATE_RANGE, cursoInserted.code);
        }

        [Fact]
        public async Task Alter_QuandoPassarDataInicioMaiorQueDataTermino_DeveRetornarCodigoDeErroAsync()
        {
            var curso = new Curso
            {
                Id = 2,
                DataInicio = new DateTime(2021, 01, 30),
                DataTermino = new DateTime(2020, 10, 01)
            };

            var cursoAltered = await cursoService.AlterAsync(curso);

            Assert.Equal(Code.INVALID_DATE_RANGE, cursoAltered.code);
        }

        [Fact]
        public async Task SetNew_QuandoPassarDataInicioMenorQueADataAtual_DeveRetornarCodigoDeErroAsync()
        {
            var curso = new Curso
            {
                Assunto = "CSharp",
                DataInicio = new DateTime(2020, 09, 18),
                DataTermino = new DateTime(2020, 12, 30),
                QuantidadeAlunos = 10,
                CodigoCategoria = 2
            };

            var cursoInserted = await cursoService.SetNewAsync(curso);

            Assert.Equal(Code.INVALID_START_DATE, cursoInserted.code);
        }

        [Fact]
        public async Task Alter_QuandoPassarDataInicioMenorQueADataAtual_DeveRetornarCodigoDeErroAsync()
        {
            var curso = new Curso
            {
                Id = 2,
                DataInicio = new DateTime(2020, 09, 18),
                DataTermino = new DateTime(2020, 12, 30),
            };

            var cursoAltered = await cursoService.AlterAsync(curso);

            Assert.Equal(Code.INVALID_START_DATE, cursoAltered.code);
        }
    }
}
