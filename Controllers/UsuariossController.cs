using EntityFramework_codefirst.Interfaces;
using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework_codefirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariossController : ControllerBase
    {
        private readonly IUsuarioRepository repositorio;

        //metodo construtor pegando repositorio
        public UsuariossController(IUsuarioRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Cadastrando usuario
        /// </summary>
        /// <param name="usuario">Dados do usuario</param>
        /// <returns>Usuario cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                //retornando Inserir do repositorio
                return Ok(repositorio.Inserir(usuario));
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
        /// Listado usuario
        /// </summary>
        /// <returns>Usuario listado</returns>
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
                //retornando mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    Message = ex.Message,
                });
            }
        }


        /// <summary>
        /// Listando usuario por id
        /// </summary>
        /// <param name="id">Pegar Usuario por id</param>
        /// <returns>Usuario listado por id</returns>
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
                        Message = "Usuario não encontrado"
                    });
                }
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                //retorna mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    Message = ex.Message,
                }); ;
            }
        }

        /// <summary>
        /// Alterando usuario
        /// </summary>
        /// <param name="id">Pegar Usuario por id</param>
        /// <param name="usuario">Guardar dados do usuario</param>
        /// <returns>Usuario alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Usuario usuario)
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
                        Message = "Usuario nao encontrado"
                    });
                }
                return NoContent();
            }
            catch (System.Exception ex)
            {
                //retorna mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    Message = ex.Message,
                }); ;
            }
        }

        /// <summary>
        /// Alterar usuario parcialmente
        /// </summary>
        /// <param name="id">Pegar Usuario por id</param>
        /// <param name="patchUsuario">Guardar usuario com dados alterados</param>
        /// <returns>Usuario parcialmente alterado</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchUsuario)
        {
            try
            {
                //validando para ver se existe no banco de dados
                if (patchUsuario == null)
                {
                    return BadRequest();
                }
                //chamando repositorio
                var usuario = repositorio.BuscarId(id);
                //validando usuario
                if (usuario == null)
                {
                    return NotFound(new
                    {
                        Message = "Usuario nao encontrado"
                    });
                }
                //alterando
                repositorio.AlterarParcial(patchUsuario, usuario);
                return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                //retorna mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    Message = ex.Message,
                }); ;
            }
        }

        /// <summary>
        /// Excluir usuario
        /// </summary>
        /// <param name="id">Pegar Usuario por id</param>
        /// <returns>Usuario excluido</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //chamando repositorio
                var busca = repositorio.BuscarId(id);
                //validando a busca do usuario
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Usuario nao encontrado"
                    });
                }
                //excluindo a busca
                repositorio.Excluir(busca);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                //retorna mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    Message = ex.Message,
                });
            }
        }
    }
}
