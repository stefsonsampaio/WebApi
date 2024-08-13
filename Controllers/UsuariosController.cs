using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Model;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuariosController : ControllerBase
{
    private readonly UserManager<AplicacaoUser> _userManager;

    public UsuariosController(UserManager<AplicacaoUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("/")]
    public async Task<IActionResult> Get()
    {
        var usuarios = await _userManager.Users.ToListAsync();
        return Ok(usuarios);
    }
}
