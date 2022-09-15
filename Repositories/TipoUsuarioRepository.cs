using EntityFramework_codefirst.Contexts;
using EntityFramework_codefirst.Interfaces;
using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework_codefirst.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        AgendasCodeContext ctx;
        public TipoUsuarioRepository(AgendasCodeContext _ctx)
        {
            ctx = _ctx;
        }
        public void Alterar(TipoUsuario tipoUsuario)
        {
            ctx.Entry(tipoUsuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcial(JsonPatchDocument patchTipoUsuario, TipoUsuario tipoUsuario)
        {
            patchTipoUsuario.ApplyTo(tipoUsuario);
            ctx.Entry(tipoUsuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public TipoUsuario BuscarId(int id)
        {
            return ctx.TipoUsuario.Find(id);
        }

        public void Excluir(TipoUsuario tipoUsuario)
        {
            ctx.TipoUsuario.Remove(tipoUsuario);
        }

        public TipoUsuario Inserir(TipoUsuario tipoUsuario)
        {
            ctx.TipoUsuario.Add(tipoUsuario);
            ctx.SaveChanges();
            return tipoUsuario;
        }

        public ICollection<TipoUsuario> Listar()
        {
            return ctx.TipoUsuario.ToList();
        }
    }
}
