using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace EntityFramework_codefirst.Interfaces
{
    public interface IUsuarioRepository
    {
        //cadastrar
        Usuario Inserir(Usuario usuario);
        //listar
        ICollection<Usuario> Listar();
        //buscar objeto por id
        Usuario BuscarId(int id);
        //alterar
        void Alterar(Usuario usuario);
        //excluir
        void Excluir(Usuario usuario);
        //alterar parcial
        void AlterarParcial(JsonPatchDocument patchUsuario, Usuario usuario);
    }
}
