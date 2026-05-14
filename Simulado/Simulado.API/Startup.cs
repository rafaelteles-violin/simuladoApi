using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simulado.Infra.Data;
using Simulado.Ioc;
using FluentValidation.AspNetCore;
using System.Globalization;
using MediatR;

namespace Simulado.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssemblyContaining<Startup>();
                x.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
            });

            services.AddDbContext<SimuladoContext>(context =>
            context.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.RegisterDependecyInjection();
            services.RegisterAutoMapper();
            services.RegisterCors();
            services.RegisterSwagger();
            services.AddIdentityConfiguration(Configuration);

            services.AddMediatR(typeof(Startup));

            //Por algum motivo n�o consigo rodar0
            //migra��o se esse trecho n�o estiver comentado
            services.AddJwtConfiguration(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();

                app.UseSwaggerConfiguration();
            //}

            app.UseCorsConfiguration();

           // app.UseHttpsRedirection(); COMENTADO PARA TESTE

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
