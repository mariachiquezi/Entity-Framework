using EntityFramework_codefirst.Contexts;
using EntityFramework_codefirst.Interfaces;
using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework_codefirst.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        AgendasCodeContext ctx;
        public PacienteRepository(AgendasCodeContext _ctx)
        {
            ctx = _ctx;
        }
        public void Alterar(Paciente paciente)
        {
            ctx.Entry(paciente).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcial(JsonPatchDocument patchPaciente, Paciente paciente)
        {
            patchPaciente.ApplyTo(paciente);
            ctx.Entry(paciente).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Paciente BuscarId(int id)
        {
            return ctx.Paciente.Find(id);
        }

        public void Excluir(Paciente paciente)
        {
            ctx.Paciente.Remove(paciente);
        }

        public Paciente Inserir(Paciente paciente)
        {
            ctx.Paciente.Add(paciente);
            ctx.SaveChanges();
            return paciente;
        }

        public ICollection<Paciente> Listar()
        {
            return ctx.Paciente.ToList();
        }
    }
}
