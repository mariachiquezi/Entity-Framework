using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace EntityFramework_codefirst.Interfaces
{
    public interface IConsultaRepository
    {  //cadastrar
        Consulta Inserir(Consulta consulta);
        //listar
        ICollection<Consulta> Listar();
        //buscar objeto por id
        Consulta BuscarId(int id);
        //alterar
        void Alterar(Consulta consulta);
        //excluir
        void Excluir(Consulta consulta);
        //alterar parcial
        void AlterarParcial(JsonPatchDocument patchConsulta, Consulta consulta);
    }
}
