using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace EntityFramework_codefirst.Interfaces
{
    public interface IMedicoRepository
    { //cadastrar
        Medico Inserir(Medico medico);
        //listar
        ICollection<Medico> Listar();
        //buscar objeto por id
        Medico BuscarId(int id);
        //alterar
        void Alterar(Medico medico);
        //excluir
        void Excluir(Medico medico);
        //alterar parcial
        void AlterarParcial(JsonPatchDocument patchMedico, Medico medico);
    }
}
