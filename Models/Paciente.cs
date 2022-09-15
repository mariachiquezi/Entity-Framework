using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_codefirst.Models
{
    public class Paciente
    {
          [Key]
          public int Id { get; set; }
          [Required]
          public string Carteirinha { get; set; }
          public DateTime DataNascimento { get; set; }
          public bool Ativo { get; set; }
          [ForeignKey("Usuario")]
          public int IdUsuario { get; set; }
          public Usuario Usuario { get; set; }

          public ICollection<Consulta> Consultass { get; set; }

    }
}
