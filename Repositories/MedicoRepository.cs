using EntityFramework_codefirst.Contexts;
using EntityFramework_codefirst.Interfaces;
using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework_codefirst.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        AgendasCodeContext ctx;
        public MedicoRepository(AgendasCodeContext _ctx)
        {
            ctx = _ctx;
        }
        public void Alterar(Medico medico)
        {
            ctx.Entry(medico).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcial(JsonPatchDocument patchMedico, Medico medico)
        {
            patchMedico.ApplyTo(medico);
            ctx.Entry(medico).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Medico BuscarId(int id)
        {
            return ctx.Medico.Find(id);
        }

        public void Excluir(Medico medico)
        {
            ctx.Medico.Remove(medico);
        }

        public Medico Inserir(Medico medico)
        {
            ctx.Medico.Add(medico);
            ctx.SaveChanges();
            return medico;
        }

        public ICollection<Medico> Listar()
        {
            return ctx.Medico.ToList();
        }
    }
}
