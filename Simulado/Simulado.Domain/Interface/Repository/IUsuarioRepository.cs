using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain.Interface.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<List<UsuarioIgreja>> ObterIgrejasDoUsuario(Guid usuarioId);
        public Task<List<Usuario>> ObterTodosUsuarios();
    }
}
