using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ToBuy.Domains;
using ToBuy.Repositories;
using ToBuy.ViewModels;

namespace ToBuy.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class LoginController : ControllerBase {
        UsuarioRepository _usuarioRepository = new UsuarioRepository ();

        private IConfiguration _config;

        public LoginController (IConfiguration config) {
            _config = config;
        }

        /// <summary>
        /// Efetua o Login do usuário
        /// </summary>
        /// <param name="login">Parâmetro login para receber o valor do objeto</param>
        /// <returns>Retorna Login efetuado</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login (LoginViewModel login) {
            try {
                IActionResult resposta = Unauthorized ();

                Usuario usuario = _usuarioRepository.AutenticarLogin (login);

                if (usuario != null) {
                    var tokenString = GerarJsonWebToken (usuario);
                    resposta = Ok (new {
                        token = tokenString
                    });
                }

                return resposta;
            } catch (Exception ex) {
                throw ex;
            }
        }

        private string GerarJsonWebToken (Usuario infoUsuario) {
            var securityKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config["Jwt:Key"]));
            var credentials = new SigningCredentials (securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new [] {
                new Claim (JwtRegisteredClaimNames.Email, infoUsuario.EmailUsuario),
                new Claim (JwtRegisteredClaimNames.Jti, infoUsuario.IdUsuario.ToString ()),
                new Claim (ClaimTypes.Role, infoUsuario.FkIdTipoUsuarioNavigation.PermissaoTipoUsuario),
                new Claim ("Role", infoUsuario.FkIdTipoUsuarioNavigation.PermissaoTipoUsuario)
            };

            var token = new JwtSecurityToken (
                issuer: _config["Jwt:Issuer"],
                audience : _config["Jwt:Issuer"],
                claims : claims,
                expires : DateTime.Now.AddMinutes (500),
                signingCredentials : credentials
            );

            return new JwtSecurityTokenHandler ().WriteToken (token);
        }
    }
}