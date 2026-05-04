using Microsoft.EntityFrameworkCore;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using Simulado.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Infra.Repositories
{
    public class IgrejaRepository : RepositoryBase<Igreja>, IIgrejaRepository
    {
        private readonly SimuladoContext _context;
        public IgrejaRepository(SimuladoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Igreja>> ObterIgrejas()
        {
            return await _context.Igreja.OrderBy(x => x.Nome).ToListAsync();
        }
    }
}
