using AutoMapper;
using Simulado.Application.ViewModel;
using Simulado.Domain.Entity;
using System.Collections.Generic;
using System.Linq;


namespace Simulado.Application.MapperConfig
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Entity to ViewModel
            CreateMap<Disciplina, DisciplinaViewModel>()
                .ForMember(Model => Model.DisciplinaId, m => m.MapFrom(entidade => entidade.Id))
                .ForMember(Model => Model.Descricao, m => m.MapFrom(entidade => entidade.Descricao));

            CreateMap<Questao, QuestaoViewModel>()
               .ForMember(Model => Model.DisciplinaId, m => m.MapFrom(entidade => entidade.Id))
               .ForMember(Model => Model.Descricao, m => m.MapFrom(entidade => entidade.Descricao))
               .ForMember(Model => Model.Alternativas, m => m.MapFrom(entidade => entidade.Alternativas.ToList()));

            CreateMap<Usuario, UsuarioViewModel>()
               .ForMember(Model => Model.Email, m => m.MapFrom(entidade => entidade.Email))
               .ForMember(Model => Model.Nome, m => m.MapFrom(entidade => entidade.Nome))
               .ForMember(Model => Model.UsuarioId, m => m.MapFrom(entidade => entidade.Id));

            CreateMap<RespostaSaldo, RespostaSaldoViewModel>()
              .ForMember(Model => Model.TotalAcerto, m => m.MapFrom(entidade => entidade.TotalAcerto))
              .ForMember(Model => Model.TotalErro, m => m.MapFrom(entidade => entidade.TotalErro))
              .ForMember(Model => Model.TotalQuestao, m => m.MapFrom(entidade => entidade.TotalQuestao))
              .ForMember(Model => Model.DisciplinaId, m => m.MapFrom(entidade => entidade.DisciplinaId))
              .ForMember(Model => Model.NomeCandidato, m => m.MapFrom(entidade => entidade.NomeCandidato));

            CreateMap<Igreja, IgrejaViewModel>()
               .ForMember(Model => Model.IgrejaId, m => m.MapFrom(entidade => entidade.Id))
               .ForMember(Model => Model.Nome, m => m.MapFrom(entidade => entidade.Nome));

            CreateMap<Disciplina, DisciplinaQuestaoViewModel>()
              .ForMember(Model => Model.DisciplinaId, m => m.MapFrom(entidade => entidade.Id))
              .ForMember(Model => Model.Disciplina, m => m.MapFrom(entidade => entidade.Descricao))
              .ForMember(Model => Model.TipoDisciplina, m => m.MapFrom(entidade => entidade.TipoDisciplina))
              .ForMember(Model => Model.Quantidade, m => m.MapFrom(entidade => entidade.Questoes
                                                          .Count(x => !x.Lixeira)));
            CreateMap<Questao, QuestaoViewModel>()
             .ForMember(Model => Model.Descricao, m => m.MapFrom(entidade => entidade.Descricao))
             .ForMember(Model => Model.Disciplina, m => m.MapFrom(entidade => entidade.Disciplina.Descricao))
             .ForMember(Model => Model.Alternativas, m => new List<AlternativaViewModel>())
             .ForMember(Model => Model.QuestaoId, m => m.MapFrom(entidade => entidade.Id));

            CreateMap<RespostaSaldo, RespostaSaldoGetViewModel>()
               .ForMember(Model => Model.Disciplina, m => m.MapFrom(entidade => entidade.Disciplina.Descricao))
               .ForMember(Model => Model.Igreja, m => m.MapFrom(entidade => entidade.Igreja.Nome))
               .ForMember(Model => Model.NomeCandidato, m => m.MapFrom(entidade => entidade.NomeCandidato))
               .ForMember(Model => Model.TotalAcerto, m => m.MapFrom(entidade => entidade.TotalAcerto))
               .ForMember(Model => Model.TotalErro, m => m.MapFrom(entidade => entidade.TotalErro))
               .ForMember(Model => Model.TotalQuestao, m => m.MapFrom(entidade => entidade.TotalQuestao))
               .ForMember(Model => Model.Identificador, m => m.MapFrom(entidade => entidade.Identificador))
               .ForMember(Model => Model.TipoDisciplina, m => m.MapFrom(entidade => entidade.Disciplina.TipoDisciplina))
               .ForMember(Model => Model.DataCadastro, m => m.MapFrom(entidade => entidade.DataCadastro.ToString("dd/MM/yyyy HH:mm")));

            //ViewModel to Entity
            CreateMap<DisciplinaViewModel, Disciplina>()
                      .ConstructUsing(disciplinaVm => new Disciplina(disciplinaVm.Descricao,
                      disciplinaVm.TipoDisciplina,
                      disciplinaVm.TotalExibicaoQuestao));

            CreateMap<QuestaoViewModel, Questao>()
                     .ConstructUsing(questaoVm => new Questao(questaoVm.DisciplinaId, questaoVm.Descricao))
                      .ForMember(questaoVm => questaoVm.Alternativas, opt => opt.Ignore());

            CreateMap<RespostaViewModel, Resposta>()
                    .ConstructUsing(respostaVm => new Resposta(respostaVm.QuestaoId, respostaVm.Aluno, respostaVm.Identificador))
                     .ForMember(questaoVm => questaoVm.RespostaAlternativas, opt => opt.Ignore());

            CreateMap<UsuarioViewModel, Usuario>()
                    .ConstructUsing(usuarioVm => new Usuario(usuarioVm.Nome, usuarioVm.Email, usuarioVm.Telefone, usuarioVm.UsuarioId));

            CreateMap<RespostaSaldoViewModel, RespostaSaldo>()
                 .ConstructUsing(vm => new RespostaSaldo(vm.NomeCandidato, vm.TotalAcerto, vm.TotalErro,
                 vm.DisciplinaId, vm.IgrejaId, vm.Identificador));

            CreateMap<IgrejaViewModel, Igreja>()
                .ConstructUsing(vm => new Igreja(vm.Nome));
        }
    }
}
