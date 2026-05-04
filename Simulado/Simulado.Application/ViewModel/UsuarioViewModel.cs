using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulado.Application.ViewModel
{
    public class UsuarioViewModel
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public List<IgrejaViewModel> Igrejas { get; set; }

        public UsuarioViewModel()
        {
            Igrejas = new List<IgrejaViewModel>();
        }

        public static UsuarioViewModel Mapear(Usuario usuario)
        {
            return new UsuarioViewModel()
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                UsuarioId = usuario.Id,
                Igrejas = usuario.UsuarioIgreja.Select(i => new IgrejaViewModel
                {
                    IgrejaId = i.IgrejaId,
                    Nome = i.Igreja.Nome
                }).ToList()
            };
        }
    }
}
