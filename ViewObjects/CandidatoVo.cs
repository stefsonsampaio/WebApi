using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewObjects
{
    public class CandidatoVo
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UrlLinkedin { get; set; }
        public string UrlGithub { get; set; }
    }
}
