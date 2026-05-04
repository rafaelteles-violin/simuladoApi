using Simulado.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Simulado.Application.ViewModel
{
    public class RespostaSaldoAvaliacaoViewModel
    {
        public string Disciplina { get; set; }
        public string Data { get; set; }
        public List<RespostaSaldoAvaliacaoDetalheViewModel> RespostaSaldoAvaliacaoDetalhe { get; set; }
    }

    public class RespostaSaldoAvaliacaoDetalheViewModel
    {
        public string NomeCandidato { get; set; }
        public int TotalAcerto { get; set; }
        public int TotalErro { get; set; }
        public int TotalQuestao { get; set; }
        public string Disciplina { get; set; }
        public string DataCadastro { get; set; }
        public string Identificador { get; set; }
        public string TipoDisciplina { get; set; }
    }
}
