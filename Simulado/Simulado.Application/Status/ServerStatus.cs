using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application.Status
{
    public class ServerStatus
    {
        public string Message { get; set; }
        public List<string> Erros { get; set; }

        public List<object> Objects { get; set; }

        public ServerStatus(string mensagem)
        {
            Message = mensagem;
        }

        public ServerStatus(List<string> erros)
        {
            Erros = new List<string>();
            Erros.AddRange(erros);
        }

        public ServerStatus(IEnumerable<object> objeto)
        {
            Objects = new List<object>();
            Objects.AddRange(objeto);
        }
    }
}
