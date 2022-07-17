using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Contato
    {
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(11)]
        public string Cpf { get; set; }

        [StringLength(10)]
        public string Senha { get; set; }

        public IFormFile Documento { get; set; }
    }
}
