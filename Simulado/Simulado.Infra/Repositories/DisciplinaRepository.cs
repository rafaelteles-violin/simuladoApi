using Microsoft.EntityFrameworkCore;
using Simulado.Domain.Entity;
using Simulado.Domain.Interface.Repository;
using Simulado.Infra.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulado.Infra.Repositories
{
    public class DisciplinaRepository : RepositoryBase<Disciplina>, IDisciplinaRepository
    {
        private readonly SimuladoContext _context;
        public DisciplinaRepository(SimuladoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Disciplina>> ObterQuantidadeDeQuestaoPorDisciplina()
        {
            return await _context.Disciplina.Include(x => x.Questoes)
                                         .AsNoTracking()
                                         .Where(x => !x.Lixeira)
                                         .OrderBy(x => x.Descricao)
                                         .ToListAsync();
        }

        public async Task<List<Disciplina>> ObterDisciplinas()
        {
            return await _context.Disciplina
                                         .AsNoTracking()
                                         .Where(x => !x.Lixeira)
                                         .OrderBy(x => x.Descricao)
                                         .ToListAsync();
        }
    }
}
