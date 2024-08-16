using System.Threading.Tasks;

public interface IAutenticacaoRepository
{
    Task<string> Authenticate(string email, string senha);
}