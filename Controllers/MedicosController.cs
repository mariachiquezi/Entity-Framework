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
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoRepository repositorio;
        //metodo construtor pegando repositorio
        public MedicosController(IMedicoRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Cadastrar medico
        /// </summary>
        /// <param name="medico">Dadps do medico</param>
        /// <returns>Medico cadastrado</returns>
      
        [HttpPost]
        public IActionResult Cadastrar(Medico medico)
        {
            try
            {
                //retornando Cadastrar do repositorio
                return Ok(repositorio.Inserir(medico));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    //retornado mensagem de erro
                    Error = "Falha na conexao",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Listando medico
        /// </summary>
        /// <returns>Medico listado</returns>
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
                return StatusCode(500, new
                {
                    //retornado mensagem de erro
                    Error = "Falha na conexao",
                    Message = ex.Message,
                });
            }
        }

        /// <summary>
        /// Listando objeto selecionado
        /// </summary>
        /// <param name="id">Pegar medico por id</param>
        /// <returns>Medico listado por id</returns>
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
                        Message = "Medico não encontrado"
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
        /// Alterar medico por id
        /// </summary>
        /// <param name="id">Pegar medico por id</param>
        /// <param name="medico">Guardar dados de medico</param>
        /// <returns>Medico alterado</returns>
        [Authorize(Roles = "Medico")]
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Medico medico)
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

                        Message = "Medico nao encontrado"
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
        /// Alterar medico parcialmente
        /// </summary>
        /// <param name="id">Pegar medico por id</param>
        /// <param name="patchMedico">Objeto que tera parte aleterada</param>
        /// <returns>Medico parcialmente alterado</returns>
         [Authorize(Roles = "Medico")]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchMedico)
        {
            try
            {
               
                //chamando repositorio
                var medico = repositorio.BuscarId(id);
                //validando medico
                if (medico == null)
                {
                    return NotFound(new
                    {
                        Message = "Medico nao encontrado"
                    });
                }
                //alterando
                repositorio.AlterarParcial(patchMedico, medico);
                return Ok(medico);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    //retornado mensagem de erro
                    Error = "Falha na conexao",
                    Message = ex.Message,
                }); ;
            }
        }

        /// <summary>
        /// Excluir um medico
        /// </summary>
        /// <param name="id">Pegar medico por id</param>
        /// <returns>Medico excluido</returns>
        [Authorize(Roles = "Medico")]
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //chamando repositorio
                var busca = repositorio.BuscarId(id);
                //validando a busca do medico
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Medico nao encontrado"
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
