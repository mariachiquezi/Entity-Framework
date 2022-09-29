using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using EntityFramework_codefirst.Contexts;
using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using EntityFramework_codefirst.Interfaces;

namespace EntityFramework_codefirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariossController : ControllerBase
    {
        private readonly  AgendasCodeContext context;
      
        public UsuariossController(AgendasCodeContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// Cadastrando usuario
        /// </summary>
        /// <param name="usuario">Dados do usuario</param>
        /// <returns>Usuario cadastrado</returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>>PostUsuario(Usuario usuario)
        {
            try
            {
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                context.Usuario.Add(usuario);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetUsuario",new {id=usuario.Id},usuario);
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
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            try
            {
                return await context.Usuario.ToListAsync();
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
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            try
            {
                var usuario = await context.Usuario.FindAsync(id);
                //validar para ver se o id inserido existe no banco
                if (usuario == null)
                {
                    return NotFound(new
                    {
                        Message = "Usuario não encontrado"
                    });
                }
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
        /// Alterando usuario
        /// </summary>
        /// <param name="id">Pegar Usuario por id</param>
        /// <param name="usuario">Guardar dados do usuario</param>
        /// <returns>Usuario alterado</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> PutUsuario(int id,Usuario usuario)
        {
                if (id != usuario.Id)
                {
                    return BadRequest();
                }
                context.Entry(usuario).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        

        /// <summary>
        /// Alterar usuario parcialmente
        /// </summary>
        /// <param name = "id" > Pegar Usuario por id</param>
        /// <param name = "patchUsuario" > Guardar usuario com dados alterados</param>
        /// <returns>Usuario parcialmente alterado</returns>
        //[HttpPatch("{id}")]
        //public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchUsuario)
        //{
        //    try
        //    {
        //        validando para ver se existe no banco de dados
        //        if (patchUsuario == null)
        //        {
        //            return BadRequest();
        //        }
        //        chamando repositorio
        //        var usuario = repositorio.BuscarId(id);
        //        validando usuario
        //        if (usuario == null)
        //        {
        //            return NotFound(new
        //            {
        //                Message = "Usuario nao encontrado"
        //            });
        //        }
        //        alterando
        //        repositorio.AlterarParcial(patchUsuario, usuario);
        //        return Ok(usuario);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        retorna mensagem de erro
        //        return StatusCode(500, new
        //        {
        //            Error = "Falha na conexao",
        //            Message = ex.Message,
        //        }); ;
        //    }
        //}

        /// <summary>
        /// Excluir usuario
        /// </summary>
        /// <param name="id">Pegar Usuario por id</param>
        /// <returns>Usuario excluido</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var usuario = await context.Usuario.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                context.Usuario.Remove(usuario);
                await context.SaveChangesAsync();

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
        private bool UsuarioExists(int id)
        {
            return context.Usuario.Any(e => e.Id == id);
        }
    }
}
