using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application.ViewModel
{
    public class IgrejaViewModel
    {
        public Guid IgrejaId { get; set; }
        public string Nome { get; set; }

        public static IgrejaViewModel Mapear(UsuarioIgreja usuario)
        {
            return new IgrejaViewModel()
            {
                IgrejaId = usuario.IgrejaId,
                Nome = usuario.Igreja.Nome
            };
        }
    }


}
