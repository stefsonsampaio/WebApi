﻿using NUnit.Framework;
using WebApi.Infra;
using WebApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Tests
{
    [TestFixture]
    public class TesteCursoRepositorio
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();

            services.AddDbContext<BusinessContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddScoped<ICursoRepositorio, CursoRepositorio>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public async Task SeCursoNaoExigeIdadeMinimaPermitirMatricular()
        {
            var context = _serviceProvider.GetRequiredService<BusinessContext>();
            var repositorio = _serviceProvider.GetRequiredService<ICursoRepositorio>();

            var aluno = new Aluno { Nome = "Aluno Teste", DataNascimento = new DateTime(2001, 11, 18) };
            var curso = new Curso { Descricao = "Curso Teste", IdadeMinimaEmAnos = null };

            context.Alunos.Add(aluno);
            context.Cursos.Add(curso);
            await context.SaveChangesAsync();

            Assert.DoesNotThrowAsync(async () => await repositorio.Matricular(aluno, curso));
        }

        [Test]
        public async Task SeCursoExigeIdadeMinimaSomentePermitirMatricularSeIdadeMinimaAtingida()
        {
            var context = _serviceProvider.GetRequiredService<BusinessContext>();
            var repositorio = _serviceProvider.GetRequiredService<ICursoRepositorio>();

            var alunoComIdadeAdequada = new Aluno { Nome = "Aluno Maior de Idade", DataNascimento = new DateTime(2000, 11, 18) };
            var alunoMenorDeIdade = new Aluno { Nome = "Aluno Menor de Idade", DataNascimento = new DateTime(2010, 11, 18) };
            var curso = new Curso { Descricao = "Curso Teste", IdadeMinimaEmAnos = 18 };

            context.Alunos.AddRange(alunoComIdadeAdequada, alunoMenorDeIdade);
            context.Cursos.Add(curso);
            await context.SaveChangesAsync();

            // Testa se a matrícula é permitida para aluno com idade adequada
            Assert.DoesNotThrowAsync(async () => await repositorio.Matricular(alunoComIdadeAdequada, curso));

            // Testa se a matrícula é negada para aluno menor de idade
            Assert.ThrowsAsync<ArgumentException>(async () => await repositorio.Matricular(alunoMenorDeIdade, curso));
        }


        [Test]
        public async Task SeAlunoNaoTemIdadeMinimaLancarExcessao()
        {
            var context = _serviceProvider.GetRequiredService<BusinessContext>();
            var repositorio = _serviceProvider.GetRequiredService<ICursoRepositorio>();

            var aluno = new Aluno { Nome = "Aluno Teste", DataNascimento = new DateTime(2015, 1, 1) };
            var curso = new Curso { Descricao = "Curso Teste", IdadeMinimaEmAnos = 18 };

            context.Alunos.Add(aluno);
            context.Cursos.Add(curso);
            await context.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () => await repositorio.Matricular(aluno, curso));
        }

        [Test]
        public async Task SeAlunoJaEstiverMatriculadoNoCursoLancarExcessao()
        {
            var context = _serviceProvider.GetRequiredService<BusinessContext>();
            var repositorio = _serviceProvider.GetRequiredService<ICursoRepositorio>();

            var aluno = new Aluno { Nome = "Aluno Teste", DataNascimento = new DateTime(2000, 1, 1) };
            var curso = new Curso { Descricao = "Curso Teste", IdadeMinimaEmAnos = null };

            context.Alunos.Add(aluno);
            context.Cursos.Add(curso);
            context.Matriculas.Add(new Matricula { Aluno = aluno, Curso = curso });
            await context.SaveChangesAsync();

            Assert.ThrowsAsync<InvalidOperationException>(async () => await repositorio.Matricular(aluno, curso));
        }
    }
}
