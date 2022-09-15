using EntityFramework_codefirst.Contexts;
using EntityFramework_codefirst.Interfaces;
using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework_codefirst.Repositories
{

    public class ConsultaRepository : IConsultaRepository
    {
        AgendasCodeContext ctx;
        public ConsultaRepository(AgendasCodeContext _ctx)
        {
            ctx = _ctx;
        }

        public void Alterar(Consulta consulta)
        {
            ctx.Entry(consulta).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcial(JsonPatchDocument patchConsulta, Consulta consulta)
        {
            patchConsulta.ApplyTo(consulta);
            ctx.Entry(consulta).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Consulta BuscarId(int id)
        {
            return ctx.Consulta.Find(id);
        }

        public void Excluir(Consulta consulta)
        {
            ctx.Consulta.Remove(consulta);
        }

        public Consulta Inserir(Consulta consulta)
        {
            ctx.Consulta.Add(consulta);
            ctx.SaveChanges();
            return consulta;
        }

        public ICollection<Consulta> Listar()
        {
            return ctx.Consulta.ToList();
        }
    }
}