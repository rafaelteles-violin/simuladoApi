using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain
{
    public static class MethodExtensions
    {
        public static string ValidarLetra(int posicao)
        {
            return posicao switch
            {
                1 => "A",
                2 => "B",
                3 => "C",
                4 => "D",
                5 => "E",
                _ => "F",
            };
        }
    }
}
