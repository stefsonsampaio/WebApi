using Microsoft.AspNetCore.Identity;

namespace WebApi.Model
{
    public class AplicacaoUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Departamento { get; set; }
    }
}
