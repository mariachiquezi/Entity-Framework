using EntityFramework_codefirst.Interfaces;
using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework_codefirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteRepository repositorio;

        //metodo construtor pegando repositorio
        public PacientesController(IPacienteRepository _repositorio)
        {
            repositorio = _repositorio;
        }


        /// <summary>
        /// Cadastrar paciente
        /// </summary>
        /// <param name="paciente">Dados do paciente</param>
        /// <returns>Paciente cadastrado</returns>
        
        [HttpPost]
        public IActionResult Cadastrar(Paciente paciente)
        {
            try
            {
                //retornando Cadastrar do repositorio
                return Ok(repositorio.Inserir(paciente));
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Listar paciente
        /// </summary>
        /// <returns>Paciente listado</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {

                //retornando Listar do repositorio
                return Ok(repositorio.Listar());
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Listar paciente por id
        /// </summary>
        /// <param name="id">Pegar paciente por id</param>
        /// <returns>Paciente listado por id</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarId(int id)
        {
            try
            {
                //chamando repositorio
                var retorno = repositorio.BuscarId(id);
                //validar para ver se o id inserido existe no banco
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Paciente não encontrado"
                    });
                }
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    Message = ex.Message,
                }); ;
            }
        }

        /// <summary>
        /// Para alterar paciente por id
        /// </summary>
        /// <param name="id">Pegar paciente por id</param>
        /// <param name="paciente">Dadoss do paciente</param>
        /// <returns>Paciente alterado</returns>
        [Authorize(Roles = "Paciente")]
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Paciente paciente)
        {
            try
            {
               
                //chamando repositorio
                var retorno = repositorio.BuscarId(id);
                //validando retorno
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Paciente nao encontrado"
                    });
                }
                return NoContent();
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    Message = ex.Message,
                }); ;
            }
        }

        /// <summary>
        /// Para alterar paciente parcialmente 
        /// </summary>
        /// <param name="id">Pegar paciente por id</param>
        /// <param name="patchPaciente">Dados do paciente parcialmente alterado</param>
        /// <returns>Paciente parcialmente alterado</returns>
        [Authorize(Roles = "Paciente")]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchPaciente)
        {
            try
            {
                //validando para ver se existe no banco de dados
                if (patchPaciente == null)
                {
                    return BadRequest();
                }
                //chamando repositorio
                var paciente = repositorio.BuscarId(id);
                //validando paciente
                if (paciente == null)
                {
                    return NotFound(new
                    {
                        Message = "Paciente nao encontrado"
                    });
                }
                //alterando
                repositorio.AlterarParcial(patchPaciente, paciente);
                return Ok(paciente);
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    Message = ex.Message,
                }); ;
            }
        }

        /// <summary>
        /// Para deletar um paciente 
        /// </summary>
        /// <param name="id">Pegar paciente por id</param>
        /// <returns>Paciente excluido</returns>
        [Authorize(Roles = "Paciente")]
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //chamando repositorio
                var busca = repositorio.BuscarId(id);
                //validando a busca do paciente
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Paciente nao encontrado"
                    });
                }
                repositorio.Excluir(busca);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    Message = ex.Message,
                });
            }
        }
    }
}
