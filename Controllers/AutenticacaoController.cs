using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoRepository _authRepo;

        public AutenticacaoController(IAutenticacaoRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Rota de autenticação.")]
        [SwaggerResponse(200, "Usuário autenticado.")]
        [SwaggerResponse(401, "Login ou senha inválidos.")]
        public async Task<IActionResult> Autenticar([FromBody] LoginModel model)
        {
            var token = await _authRepo.Authenticate(model.Email, model.Senha);
            if (token == null)
            {
                return Unauthorized("Login ou senha inválidos");
            }

            return Ok(new { token });
        }
    }
}

public class LoginModel
{
    public string Email { get; set; }
    public string Senha { get; set; }
}
