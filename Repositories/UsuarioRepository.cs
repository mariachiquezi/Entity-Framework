using EntityFramework_codefirst.Contexts;
using EntityFramework_codefirst.Interfaces;
using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework_codefirst.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        AgendasCodeContext ctx;
        public UsuarioRepository(AgendasCodeContext _ctx)
        {
            ctx = _ctx;
        }
        public void Alterar(Usuario usuario)
        {
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void AlterarParcial(JsonPatchDocument patchUsuario, Usuario usuario)
        {
            patchUsuario.ApplyTo(usuario);
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public Usuario BuscarId(int id)
        {
            return ctx.Usuario.Find(id);
        }

        public void Excluir(Usuario usuario)
        {
            ctx.Usuario.Remove(usuario);
            ctx.SaveChanges();
        }

        public Usuario Inserir(Usuario usuario)
        {
            ctx.Usuario.Add(usuario);
            ctx.SaveChanges();
            return usuario;
        }

        public ICollection<Usuario> Listar()
        {
            return ctx.Usuario.ToList();
        }
    }
}
