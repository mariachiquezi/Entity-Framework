using EntityFramework_codefirst.Interfaces;
using EntityFramework_codefirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework_codefirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ILoginRepository repositorio;

        public LoginsController(ILoginRepository _repositorio)
        {
            repositorio = _repositorio;
        }


        [HttpPost]
        public IActionResult Logar(string email , string senha) 
        {
            //verficando se usuario ja tem uma conta para autorizar
            var login = repositorio.Logar(email,senha);
            if (login == null)
                return Unauthorized();

            //retornando token em string
            return Ok(new { token = login });
        }
    }
}
