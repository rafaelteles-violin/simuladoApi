using Microsoft.EntityFrameworkCore;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using Simulado.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulado.Infra.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly SimuladoContext _context;
        public UsuarioRepository(SimuladoContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<UsuarioIgreja>> ObterIgrejasDoUsuario(Guid usuarioId)
        {
            return _context.UsuarioIgreja.Include(x => x.Igreja)
                                         .Where(x => x.UsuarioId == usuarioId)
                                         .ToListAsync();
        }

        public Task<List<Usuario>> ObterTodosUsuarios()
        {
            return _context.Usuario.Include(x => x.UsuarioIgreja)
                                   .ThenInclude(x=>x.Igreja)
                                   .ToListAsync();
        }
    }
}
