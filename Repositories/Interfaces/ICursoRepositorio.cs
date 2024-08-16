using WebApi.Infra;

public interface ICursoRepositorio
{
    Task Matricular(Aluno aluno, Curso curso);
}
