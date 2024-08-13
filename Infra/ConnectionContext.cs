using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Model;
using WebApi.ViewObjects;

namespace WebApi.Infra
{
    public class ConnectionContext : IdentityDbContext<AplicacaoUser>
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options)
        {
        }

        public DbSet<CandidatoVo> Candidato {  get; set; }
    }
}
