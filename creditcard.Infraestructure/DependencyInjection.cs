using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using creditcard.Infraestructure.DbContext.Interfaces;
using creditcard.Infraestructure.DbContext;
using creditcard.application.Interfaces;
using creditcard.Infraestructure.Queries;
using creditcard.Infraestructure.HealthChecks;
using creditcard.Infraestructure.Commands;

namespace creditcard.Infraestructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAppDbContext, AppDbContext>();
            services.AddTransient<IConfiguracionesQueries, ConfiguracionesQueries>();
            services.AddTransient<IHealthCheckQueries, DatabaseHealthCheck>();
            services.AddTransient<ILogsCommand, AddLogsCommand>();
            services.AddTransient<IEstadoCuentaQueries, EstadoCuentaQueries>();
            services.AddTransient<IInfoClienteQueries, InfoClientesQueries>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
