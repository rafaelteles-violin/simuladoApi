using Microsoft.Extensions.DependencyInjection;
using Simulado.Application.MapperConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Ioc
{
    public static class AutoMapperConfiguration
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());

            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
