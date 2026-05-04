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
    public class RespostaSaldoRepository : RepositoryBase<RespostaSaldo>, IRespostaSaldoRepository
    {
        private readonly SimuladoContext _context;
        public RespostaSaldoRepository(SimuladoContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<RespostaSaldo>> ObterRespostaSaldo()
        {
            return await _context.RespostaSaldo.AsNoTracking()
                                          .Include(x => x.Disciplina)
                                          .Include(x => x.Igreja)
                                          .Where(x => !x.Lixeira)                        
                                          .OrderByDescending(x => x.DataCadastro)
                                          .ToListAsync();
        }

        public async Task<List<RespostaSaldo>> ObterRespostaSaldoPorIgrejas(List<Guid> idsIgrejas)
        {
            return await _context.RespostaSaldo.AsNoTracking()
                                          .Include(x => x.Disciplina)
                                          .Include(x => x.Igreja)
                                          .Where(t => idsIgrejas.Contains(t.IgrejaId) && !t.Lixeira)
                                          .OrderByDescending(x => x.DataCadastro)
                                          .ToListAsync();
        }

        public async Task<List<RespostaSaldo>> ObterRespostaSaldoAvaliacao()
        {
            return await _context.RespostaSaldo.AsNoTracking()
                                          .Include(x => x.Disciplina)
                                          .Include(x => x.Igreja)
                                          .Where(t => !t.Lixeira &&
                                          t.Disciplina.TipoDisciplina == Domain.Enum.TipoDisciplinaEnum.AVALIACAO)
                                          .OrderByDescending(x => x.DataCadastro)
                                          .ToListAsync();
        }
    }
}
