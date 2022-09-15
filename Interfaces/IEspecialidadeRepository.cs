using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace EntityFramework_codefirst.Interfaces
{
    public interface IEspecialidadeRepository
    { 
        //cadastrar
        Especialidade Inserir(Especialidade especialidade);
        //listar
        ICollection<Especialidade> Listar();
        //buscar objeto por id
        Especialidade BuscarId(int id);
        //alterar
        void Alterar(Especialidade especialidade);
        //excluir
        void Excluir(Especialidade especialidade);
        //alterar parcial
        void AlterarParcial(JsonPatchDocument patchEspecialidade, Especialidade especialidade);
    }
}
