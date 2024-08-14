using Swashbuckle.AspNetCore.Filters;

namespace WebApi.Infra
{
    public class UsuarioExampleRequest : IExamplesProvider<LoginModel>
    {
        public LoginModel GetExamples()
        {
            return new LoginModel
            {
                Email = "candidato@nd.com.br",
                Senha = "Senha@Forte123#"
            };
        }
    }
}
