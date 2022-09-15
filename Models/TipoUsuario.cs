 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;

namespace EntityFramework_codefirst.Models
{
        public class TipoUsuario
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public string Tipo { get; set; }

            public ICollection<TipoUsuario> TipoUsuarios { get; set; }
        }
}
