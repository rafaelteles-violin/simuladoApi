using Simulado.Domain;
using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application.ViewModel
{
    public class QuestaoViewModel
    {
        public Guid DisciplinaId { get; set; }
        public string Disciplina { get; set; }
        public Guid QuestaoId { get; set; }
        public string Descricao { get; set; }
        public List<AlternativaViewModel> Alternativas { get; set; }

        public static QuestaoViewModel Mapear(Questao questao)
        {
            var questaoVm = new QuestaoViewModel()
            {
                Descricao = questao.Descricao,
                QuestaoId = questao.Id,
                Disciplina = questao.Disciplina.Descricao,
                DisciplinaId = questao.Disciplina.Id,
                Alternativas = new List<AlternativaViewModel>()
            };

            var listAlternativa = new List<AlternativaViewModel>(questao.Alternativas.Count);

            foreach (var alternativa in questao.Alternativas)
            {
                listAlternativa.Add(new AlternativaViewModel()
                {
                    AlternativaId = alternativa.Id,
                    Correta = alternativa.Correta,
                    Descricao = alternativa.Descricao,
                    Posicao = alternativa.Posicao,
                    Letra = MethodExtensions.ValidarLetra(alternativa.Posicao)
                });
            }

            var alternativaOrdenada = listAlternativa.OrderBy(x => x.Posicao).ToList();
            questaoVm.Alternativas.AddRange(alternativaOrdenada);

            return questaoVm;
        }
    }
}
