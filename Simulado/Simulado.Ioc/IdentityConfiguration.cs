
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simulado.Infra.Data;

namespace Simulado.Ioc
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<SimuladoContext>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false; //Obriga a ter um numero na senha
                options.Password.RequiredLength = 6; // valor default 6 digitos
                options.Password.RequiredUniqueChars = 1;         // permite repetição
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            return services;
        }
    }
}
