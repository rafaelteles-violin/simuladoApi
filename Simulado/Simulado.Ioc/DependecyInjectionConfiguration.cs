using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Simulado.Application;
using Simulado.Application.Commands.DisciplinaCommand;
using Simulado.Application.Interface;
using Simulado.Domain.Interface.Repository;
using Simulado.Domain.Interface.Service;
using Simulado.Infra.Repositories;
using Simulado.Service;

namespace Simulado.Ioc
{
    public static class DependecyInjectionConfiguration
    {
        public static void RegisterDependecyInjection(this IServiceCollection services)
        {
            services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
            services.AddScoped<IQuestaoRepository, QuestaoRepository>();
            services.AddScoped<IAlternativaRepository, AlternativaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRespostaRepository, RespostaRepository>();
            services.AddScoped<IAlternativaRespostaRepository, AlternativaRespostaRepository>();
            services.AddScoped<IRespostaSaldoRepository, RespostaSaldoRepository>();
            services.AddScoped<IIgrejaRepository, IgrejaRepository>();

            services.AddScoped<IDisciplinaApplication, DisciplinaApplication>();
            services.AddScoped<IQuestaoApplication, QuestaoApplication>();
            services.AddScoped<IUsuarioApplication, UsuarioApplication>();
            services.AddScoped<IRespostaApplication, RespostaApplication>();
            services.AddScoped<IRespostaSaldoApplication, RespostaSaldoApplication>();
            services.AddScoped<IIgrejaApplication, IgrejaApplication>();

            services.AddScoped<IServiceQuestao, ServiceQuestao>();
            services.AddScoped<IServiceResposta, ServiceResposta>();


            //Commands
            services.AddScoped<IRequestHandler<AdicionarDisciplinaCommand, ValidationResult>, DisciplinaCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarDisciplinaCommand, ValidationResult>, DisciplinaCommandHandler>();

            
        }
    }
}
