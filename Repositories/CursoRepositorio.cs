using Microsoft.EntityFrameworkCore;
using WebApi.Infra;

namespace WebApi.Repositories
{
    public class CursoRepositorio : ICursoRepositorio
    {
        public readonly BusinessContext _context;

        public CursoRepositorio(BusinessContext context)
        {
            _context = context;
        }

        public async Task Matricular(Aluno aluno, Curso curso)
        {
            var idadeAluno = DateTime.Now.Year - aluno.DataNascimento.Year;
            var cursoExistente = await _context.Cursos
                .Include(c => c.Matriculas)
                .FirstOrDefaultAsync(c => c.Id == curso.Id);

            if (cursoExistente == null)
            {
                throw new ArgumentException("Curso não encontrado.");
            }

            if (cursoExistente.IdadeMinimaEmAnos.HasValue && idadeAluno < cursoExistente.IdadeMinimaEmAnos.Value)
            {
                throw new ArgumentException("Aluno não possui idade mínima para matricular nesse curso.");
            }

            if (cursoExistente.Matriculas.Any(m => m.AlunoId == aluno.Id))
            {
                throw new InvalidOperationException("Aluno já está matriculado nesse curso.");
            }

            _context.Matriculas.Add(new Matricula { Aluno = aluno, Curso = curso });
            await _context.SaveChangesAsync();
        }
    }
}
