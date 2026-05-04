using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application.ViewModel
{
    public class AlternativaViewModel
    {
        public Guid AlternativaId { get; set; }
        public string Descricao { get; set; }
        public int Posicao { get; set; }
        public bool Correta { get; set; }
        public bool Selecionada { get; set; }
        public string Letra { get; set; }
    }
}
