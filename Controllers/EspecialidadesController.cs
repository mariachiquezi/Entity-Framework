using EntityFramework_codefirst.Interfaces;
using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework_codefirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IEspecialidadeRepository repositorio;
        //metodo construtor pegando repositorio
        public EspecialidadesController(IEspecialidadeRepository _repositorio)
        {
            repositorio = _repositorio;
        }

        /// <summary>
        /// Cadastramento de especialidades
        /// </summary>
        /// <param name="especialidade">Dados de especialidade</param>
        /// <returns>Especialidade cadastrada</returns>
        [HttpPost]
        public IActionResult Cadastrar(Especialidade especialidade)
        {
            try
            {
                //retornando Cadastrar do repositorio
                return Ok(repositorio.Inserir(especialidade));
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
        /// Listando especialidade
        /// </summary>
        /// <returns>Especialidade listada</returns>
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
        /// Listando especialidade por id
        /// </summary>
        /// <param name="id">Pegar especialidade por id</param>
        /// <returns>Especialidade selecionada</returns>
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
                        Message = "Especialidade não encontrada"
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
        /// Alterar especialidade por id
        /// </summary>
        /// <param name="id">Pegar especialidade por id</param>
        /// <param name="especialidade">Dados de especialidades</param>
        /// <returns>Especialidade alterada</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Especialidade especialidade)
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
                        Message = "Especialidade nao encontrada"
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
        /// Alterar objeto parcialmente
        /// </summary>
        /// <param name="id">Pegar especialidade por id</param>
        /// <param name="patchEspecialidade">Objeto que tera alguma parte alterada</param>
        /// <returns>Especialidade alterado parcialmente</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchEspecialidade)
        {
            try
            {
                //validando para ver se existe no banco de dados
                if (patchEspecialidade == null)
                {
                    return BadRequest();
                }
                //chamando repositorio
                var especialidade = repositorio.BuscarId(id);
                //validando especialidade
                if (especialidade == null)
                {
                    return NotFound(new
                    {
                        Message = "Especialidade nao encontrada"
                    });
                }
                //alterando
                repositorio.AlterarParcial(patchEspecialidade, especialidade);
                return Ok(especialidade);
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
        /// Excluir alguma especialidade
        /// </summary>
        /// <param name="id">Pegar especialidade por id</param>
        /// <returns>Especialidade excluida</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //chamando repositorio
                var busca = repositorio.BuscarId(id);
                //validando a busca da especialidade
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Especialidade nao encontrada"
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

