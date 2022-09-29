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
    public class ConsultasController : ControllerBase
    {
        private readonly IConsultaRepository repositorio;

        //metodo construtor pegando repositorio
        public ConsultasController(IConsultaRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Cadastramento de consulta
        /// </summary>
        /// <param name="consulta">Dados da consulta</param>
        /// <returns>Dados cadastrados</returns>
        
        [HttpPost]
        public IActionResult Cadastrar(Consulta consulta)
        {
            try
            {
                //retornando Cadastrar do repositorio
                return Ok(repositorio.Inserir(consulta));
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
        /// Listando consulta
        /// </summary>
        /// <returns>Consulta listada</returns>
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
        /// Listar uma consulta por id
        /// </summary>
        /// <param name="id">Pegar consulta por id</param>
        /// <returns>Consulta selecionada</returns>
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
                        Message = "Consulta não encontrada"
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
        /// Alterar alguma consulta por id 
        /// </summary>
        /// <param name="id">Pegar consulta por id</param>
        /// <param name="consulta">Guardar dados consulta</param>
        /// <returns>Consulta alterada</returns>
        [Authorize(Roles = "Adm")]
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Consulta consulta)
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
                        Message = "Consulta nao encontrado"
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
        /// Alterar uma parte do objeto
        /// </summary>
        /// <param name="id">Pegar consulta por id</param>
        /// <param name="patchConsulta">Objeto que tera a parte alterada</param>
        /// <returns>Objeto parcialmente alterado</returns>
        [Authorize(Roles = "Adm")]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchConsulta)
        {
            try
            {
                //validando para ver se existe no banco de dados
                if (patchConsulta == null)
                {
                    return BadRequest();
                }
                //chamando repositorio
                var consulta = repositorio.BuscarId(id);
                //validando consulta
                if (consulta == null)
                {
                    return NotFound(new
                    {
                        Message = "Consulta nao encontrada"
                    });
                }
                //alterando
                repositorio.AlterarParcial(patchConsulta, consulta);
                return Ok(consulta);
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
        /// Para deletar alguma consulta
        /// </summary>
        /// <param name="id">Pegar consulta por id</param>
        /// <returns>Consulta excluida</returns>
        [Authorize(Roles = "Adm")]
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //chamando repositorio
                var busca = repositorio.BuscarId(id);
                //validando a busca da consulta
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Consulta nao encontrada"
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

