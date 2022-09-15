using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFramework_codefirst.Models
{
    public class Especialidade
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Categoria { get; set; }

        public ICollection<Medico> Medicos { get; set; }
        public ICollection<Paciente> Pacientes { get; set; }
    }

}