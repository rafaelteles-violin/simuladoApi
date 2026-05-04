using Simulado.Domain;
using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Application.ViewModel
{
    public class RespostaViewModel
    {
        public Guid RespostaId { get; set; }
        public Guid QuestaoId { get; set; }
        public string Aluno { get; set; }
        public string Questao { get; set; }
        public string Disciplina { get; set; }
        public string Identificador { get; set; }
        public List<AlternativaRespostaViewModel> AlternativaResposta { get; set; }

        public static RespostaViewModel Mapear(Resposta resposta)
        {
            var questaoVm = new RespostaViewModel()
            {
                Aluno = resposta.Aluno,
                RespostaId = resposta.Id,
                Questao = resposta.Questao.Descricao,
                Disciplina = resposta.Questao.Disciplina.Descricao,
                Identificador = resposta.Identificador,
                AlternativaResposta = new List<AlternativaRespostaViewModel>()
            };

            var listAlternativa = new List<AlternativaRespostaViewModel>();

            foreach (var alternativa in resposta.RespostaAlternativas)
            {
                listAlternativa.Add(new AlternativaRespostaViewModel()
                {
                    AlternativaId = alternativa.Id,
                    Correta = alternativa.Correta,
                    Descricao = alternativa.Descricao,
                    Posicao = alternativa.Posicao,
                    Selecionada = alternativa.Selecionada,
                    Letra = MethodExtensions.ValidarLetra(alternativa.Posicao)
                });
            }

            var alternativaOrdenada = listAlternativa.OrderBy(x => x.Posicao).ToList();
            questaoVm.AlternativaResposta.AddRange(alternativaOrdenada);

            return questaoVm;
        }
    }

}
