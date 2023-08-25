using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebAPI.Models;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateTokenIdentity")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] Login login)
        {
            if(string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return Unauthorized();
            }

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var userCurrent = await _userManager.FindByEmailAsync(login.Email);
                var idUser = userCurrent.Id;

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JWTSecuritykey.Create("Secret_Key-12345678"))
                    .AddSubject("Empresa - Canal Dev Net Core")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaim("idUser", idUser)
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);
            }
            
            return Unauthorized();            
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUserIdentity")]
        public async Task<IActionResult> AddUserIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) && string.IsNullOrWhiteSpace(login.Password))
                return Ok("Preencha os dados corretamente!");

            var user = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                CPF = login.Cpf,
                TypeUser = TypeUser.Comum
            };

            var result = await _userManager.CreateAsync(user, login.Password);
            if (result.Errors.Any())
                Ok(result.Errors);

            // TODO: Implementar posteriormente esta confirmação
            //forçando a validação do email
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code)); //enviar este code por email

            //Na tela de confirmação
            //Imputar o token e fazer a confirmação
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultConfirmation = await _userManager.ConfirmEmailAsync(user, code);

            if (resultConfirmation.Succeeded)
                return Ok("Usuário logado com sucesso!");
            else
                return Ok("Erro ao confirmar usuário!");
        }
    }
}
