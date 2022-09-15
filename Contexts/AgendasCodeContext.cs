using EntityFramework_codefirst.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework_codefirst.Contexts
{
    public class AgendasCodeContext:DbContext
    {
        public AgendasCodeContext(DbContextOptions<AgendasCodeContext> options) : base(options)
        {

        }

        //apontando as classes
        public DbSet<Consulta> Consulta { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Especialidade> Especialidade { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
