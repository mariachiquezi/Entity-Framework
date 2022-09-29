using EntityFramework_codefirst.Contexts;
using EntityFramework_codefirst.Interfaces;
using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework_codefirst.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        AgendasCodeContext ctx;
        public EspecialidadeRepository(AgendasCodeContext _ctx)
        {
            ctx = _ctx;
        }
        public void Alterar(Especialidade especialidade)
        {
            ctx.Entry(especialidade).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcial(JsonPatchDocument patchEspecialidade, Especialidade especialidade)
        {
            patchEspecialidade.ApplyTo(especialidade);
            ctx.Entry(especialidade).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Especialidade BuscarId(int id)
        {
            return ctx.Especialidade.Find(id);
        }

        public void Excluir(Especialidade especialidade)
        {
            ctx.Especialidade.Remove(especialidade);
            ctx.SaveChanges();
        }

        public Especialidade Inserir(Especialidade especialidade)
        {
            ctx.Especialidade.Add(especialidade);
            ctx.SaveChanges();
            return especialidade;
        }

        public ICollection<Especialidade> Listar()
        {
            return ctx.Especialidade.ToList();
        }
    }
}
