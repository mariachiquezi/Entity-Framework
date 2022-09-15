using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_codefirst.Models

{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { set; get; }



        [ForeignKey("TipoUsuario")]
        public int IdTipoUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public ICollection<Medico> Medicos { get; set; }
    }
}
