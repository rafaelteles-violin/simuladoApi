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
    public class RespostaRepository : RepositoryBase<Resposta>, IRespostaRepository
    {
        private readonly SimuladoContext _context;
        public RespostaRepository(SimuladoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Resposta> ObterRespostaComAlternativas(Guid questaoId)
        {
            return await _context.Resposta.Include(x => x.RespostaAlternativas)
                                  .Include(x => x.Questao)
                                  .ThenInclude(x => x.Disciplina)
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(x => x.Id == questaoId && !x.Lixeira);
        }

        public async Task<List<Resposta>> ObterTodasRespostasComAlternativas()
        {
            return await _context.Resposta.Include(x => x.Questao)
                                          .ThenInclude(x => x.Disciplina)
                                          .AsNoTracking()
                                          .Where(x => !x.Lixeira)
                                          .ToListAsync();
        }

        public async Task<List<Resposta>> ObterRespostasPorIdentificador(string identificador)
        {
            return await _context.Resposta.Include(x => x.Questao)
                                          .ThenInclude(x => x.Disciplina)
                                          .Include(x => x.RespostaAlternativas)
                                          .AsNoTracking()
                                          .Where(x => x.Identificador == identificador)
                                          .OrderBy(x=> new Guid())
                                          .ToListAsync();
        }
    }
}
