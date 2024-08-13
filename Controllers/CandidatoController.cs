using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewObjects;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var candidato = new CandidatoVo
            {
                Nome = "Stefson",
                UrlLinkedin = "teste",
                UrlGithub = "teste"
            };

            return Ok(candidato);
        }
    }
}
