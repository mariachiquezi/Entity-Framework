using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace EntityFramework_codefirst.Interfaces
{
    public interface IPacienteRepository
    { 
        //cadastrar
        Paciente Inserir(Paciente paciente);
        //listar
        ICollection<Paciente> Listar();
        //buscar objeto por id
        Paciente BuscarId(int id);
        //alterar
        void Alterar(Paciente paciente);
        //excluir
        void Excluir(Paciente paciente);
        //alterar parcial
        void AlterarParcial(JsonPatchDocument patchPaciente, Paciente paciente);
    }
}
