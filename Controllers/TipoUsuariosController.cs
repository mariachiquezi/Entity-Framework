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
    public class TipoUsuariosController : ControllerBase
    {
        private readonly ITipoUsuarioRepository repositorio;
        //metodo construtor pegando repositorio
        public TipoUsuariosController(ITipoUsuarioRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Cadastrando TipoUsuario
        /// </summary>
        /// <param name="tipoUsuario">Dados de TipoUsuario</param>
        /// <returns>TipoUsuario cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(TipoUsuario tipoUsuario)
        {
            try
            {
                //retornando Cadastrar do repositorio
                return Ok(repositorio.Inserir(tipoUsuario));
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
        /// Listando TipoUsuario
        /// </summary>
        /// <returns>TipoUsuario cadastrado</returns>
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
        /// Listando TipoUsuario por id
        /// </summary>
        /// <param name="id">Pegar TipoUsuario por id</param>
        /// <returns>TipoUsuario listado por id</returns>
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
                        Message = "TipoUsuario não encontrado"
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
        /// Alterar TipoUsuario
        /// </summary>
        /// <param name="id">Pegar TipoUsuario por id</param>
        /// <param name="tipoUsuario">Guardar dados de TipoUsuario</param>
        /// <returns>TipoUsuario alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, TipoUsuario tipoUsuario)
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
                        Message = "Nao encontrado"
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
        /// Alterar TipoUsuario parcialmente
        /// </summary>
        /// <param name="id">Pegar TipoUsuario por id</param>
        /// <param name="patchTipoUsuario">Guardar dados</param>
        /// <returns>TipoUsuario parcialmente alterado</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchTipoUsuario)
        {
            try
            {
                //validando para ver se existe no banco de dados
                if (patchTipoUsuario == null)
                {
                    return BadRequest();
                }
                //chamando repositorio
                var tipoUsuario = repositorio.BuscarId(id);
                //validando tipoUsuario
                if (tipoUsuario == null)
                {
                    return NotFound(new
                    {
                        Message = "Nao encontrado"
                    });
                }
                //alterando
                repositorio.AlterarParcial(patchTipoUsuario, tipoUsuario);
                return Ok(tipoUsuario);
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
        /// Excluir TipoUsuario
        /// </summary>
        /// <param name="id">Pegar TipoUsuario por id</param>
        /// <returns>TipoUsuario excluido</returns>
       
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //chamando repositorio
                var busca = repositorio.BuscarId(id);
                //validando a busca do tipoUsuario
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Nao encontrado"
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
