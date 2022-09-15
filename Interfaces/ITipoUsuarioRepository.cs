using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace EntityFramework_codefirst.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        //cadastrar
        TipoUsuario Inserir(TipoUsuario tipoUsuario);
        //listar
        ICollection<TipoUsuario> Listar();
        //buscar objeto por id
        TipoUsuario BuscarId(int id);
        //alterar
        void Alterar(TipoUsuario tipoUsuario);
        //excluir
        void Excluir(TipoUsuario tipoUsuario);
        //alterar parcial
        void AlterarParcial(JsonPatchDocument patchTipoUsuario, TipoUsuario tipoUsuario);
    }
}
