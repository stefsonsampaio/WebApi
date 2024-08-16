using Microsoft.EntityFrameworkCore;

namespace WebApi.Infra
{
    public class BusinessContext : DbContext
    {
        public BusinessContext(DbContextOptions<BusinessContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }   
    }

    public class Curso
    {
        public int Id {  set; get; }
        public string Descricao { get; set; }
        public int? IdadeMinimaEmAnos { get; set; }

        public ICollection<Matricula> Matriculas { get; set; }
    }

    public class Matricula
    {
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}
