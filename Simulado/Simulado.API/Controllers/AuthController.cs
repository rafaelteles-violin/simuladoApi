using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Simulado.API.Model;
using Simulado.Application.Interface;
using Simulado.Application.ViewModel;
using Simulado.Ioc.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;
        private readonly IUsuarioApplication _usuarioApplication;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IUsuarioApplication usuarioApplication,
                              IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
            _usuarioApplication = usuarioApplication;
        }

        [HttpPost("Registrar")]
        //[Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> RegistrarUsuario(RegistrarUsuarioViewModel usuarioVm)
        {
            var user = new IdentityUser()
            {
                UserName = usuarioVm.Nome,
                Email = usuarioVm.Nome
            };

            var result = await _userManager.CreateAsync(user, usuarioVm.Senha);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                await AdicionarPermissao(usuarioVm, user);

                if (usuarioVm.Permissao == "ADMINISTRADOR")
                    return CustomResponse("Registro efetuado com sucesso", null);

                var newUserVm = new UsuarioViewModel()
                {
                    Email = usuarioVm.Email,
                    Nome = usuarioVm.Nome,
                    Telefone = usuarioVm.Telefone,
                    UsuarioId = Guid.Parse(user.Id),
                    Igrejas = usuarioVm.Igrejas
                };

                await _usuarioApplication.Adicionar(newUserVm);

                return CustomResponse("Registro efetuado com sucesso", null);
            }

            return BadRequest(result.Errors.FirstOrDefault().Description.ToString());
        }



        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginUsuarioViewModel usuario)
        {
            var result = await _signInManager.PasswordSignInAsync(usuario.Nome, usuario.Senha, false, false);

            if (result.Succeeded)
                return Ok(await GerarToken(usuario.Nome));

            return BadRequest("Usuário ou Senha inválido");
        }

        [HttpPost("UpdatePassword")]
        [Authorize]
        public async Task<ActionResult> UpdatePassword(PasswordViewModel password)
        {
            var user = await _userManager.FindByIdAsync(password.Id.ToString());

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, password.Senha);

            if (result.Succeeded)
                return CustomResponse("Senha atualizada com sucesso", null);

            return BadRequest("Usuário ou Senha inválido");
        }


        [HttpPost("CreateRole")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> CreateRole(RoleViewModel regra)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = regra.role
            };

            var regraCadastrada = await _roleManager.FindByNameAsync(regra.role);
            if (regraCadastrada != null)
                return CustomResponse("Essa regra já existe", null);


            IdentityResult result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return CustomResponse("Regra adicionada com sucesso", null);

            return BadRequest(result.Errors.FirstOrDefault().Description.ToString());
        }


        [HttpGet("ObterPermissoes")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<List<RoleViewModel>> ObterPermissoes()
        {
            var roles = _roleManager.Roles.Select(x => x.Name).ToList();

            var rolesVm = new List<RoleViewModel>();

            foreach (var role in roles)
                rolesVm.Add(new RoleViewModel { role = role });

            return await Task.FromResult(rolesVm);
        }


        private async Task AdicionarPermissao(RegistrarUsuarioViewModel usuarioVm, IdentityUser user)
        {
            var applicationRole = await _roleManager.FindByNameAsync(usuarioVm.Permissao);

            if (applicationRole != null)
                await _userManager.AddToRoleAsync(user, applicationRole.Name);
        }


        private async Task<LoginReponseViewModel> GerarToken(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var igrejas = await _usuarioApplication.ObterIgrejasDoUsuario(Guid.Parse(user.Id));

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginReponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = Guid.Parse(user.Id),
                    Nome = user.UserName,
                    Email = user.Email,
                    Perfil = userRoles.First(),
                    Igrejas = igrejas.Select(x => x.IgrejaId).ToList(),
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }


        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
