using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using creditcard.application.UseCases.Interfaces;
using creditcard.application.UseCases;

namespace creditcard.application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IConfigurationesUseCases, ConfiguracionesUseCases>();
            services.AddTransient<IhealthCheckUseCase, HealthCheckUseCases>();
            services.AddTransient<ILogsUseCases, LogsUseCases>();
            services.AddTransient<IEstadoCuentaUseCases, EstadoCuentaUseCase>();
            services.AddTransient<IInfoClientesUseCases, InfoClientesuseCases>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
