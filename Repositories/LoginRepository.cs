using EntityFramework_codefirst.Contexts;
using EntityFramework_codefirst.Interfaces;
using EntityFramework_codefirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace EntityFramework_codefirst.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AgendasCodeContext ctx;
        public LoginRepository(AgendasCodeContext _ctx)
        {
            ctx = _ctx;
        }

        public string Logar(string email, string senha)
        {
            // pesquisar por email
            var usuario = ctx.Usuario
                .Include(u=>u.TipoUsuario)
                .FirstOrDefault(x => x.Email == email);
            if (usuario != null && usuario.Senha != null && usuario.Senha.Contains("$2b$"))
            {
                //conferindo se tem usuario no banco de dados
                bool conferirSenha = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);
                if (conferirSenha)
                {
                    //criando credenciais
                    var minhasClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                         new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString()),
                         new Claim (ClaimTypes.Role,usuario.TipoUsuario.Tipo),
                    };
                    //criando chaves
                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("cripto-chave-autenticacao"));

                    //criando credenciais 
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    //gerando token
                    var meuToken = new JwtSecurityToken(
                       issuer: "cripto.webAPI",
                       audience: "cripto.webAPI",
                       claims: minhasClaims,
                       expires: DateTime.Now.AddMinutes(30),
                       signingCredentials: creds
                        );
                    //token
                    return new JwtSecurityTokenHandler().WriteToken(meuToken);
                }
            }
            return null;
        }

       
    }
}
    
