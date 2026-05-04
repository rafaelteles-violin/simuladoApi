using Simulado.Domain.Entity;
using System;

namespace Simulado.Application.ViewModel
{
    public class RespostaSaldoViewModel
    {
        public string NomeCandidato { get; set; }
        public int TotalAcerto { get; set; }
        public int TotalErro { get; set; }
        public int TotalQuestao { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid IgrejaId { get; set; }
        public string Identificador { get; set; }
    }

    public class RespostaSaldoGetViewModel
    {
        public string NomeCandidato { get; set; }
        public int TotalAcerto { get; set; }
        public int TotalErro { get; set; }
        public int TotalQuestao { get; set; }
        public string Disciplina { get; set; }
        public string DataCadastro { get; set; }
        public string Igreja { get; set; }
        public string Identificador { get; set; }
        public string TipoDisciplina { get; set; }
    }
}
