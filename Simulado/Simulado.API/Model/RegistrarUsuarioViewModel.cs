using Simulado.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Simulado.API.Model
{
    public class RegistrarUsuarioViewModel
    {
        public Guid Id { get; set; }
        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmaSenha { get; set; }

        public string Permissao { get; set; }

        public string Telefone { get; set; }

        public List<IgrejaViewModel> Igrejas { get; set; }

    }

    public class LoginUsuarioViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
    }

    public class UserTokenViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Perfil { get; set; }
        public List<Guid> Igrejas { get; set; } = new List<Guid>();
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }

    public class LoginReponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }

    public class ClaimViewModel
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class RoleViewModel
    {
        public string role { get; set; }
    }

    public class PasswordViewModel
    {
        public Guid Id { get; set; }
        public string Senha { get; set; }
    }
}
