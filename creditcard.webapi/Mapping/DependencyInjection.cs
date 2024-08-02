using creditcard.application.UseCases.Interfaces;
using creditcard.application.UseCases;
using System.Reflection;
using FluentValidation;
using creditcard.webapi.Models.Request;
using FluentValidation.AspNetCore;
using creditcard.webapi.Middlewares.Validations;

namespace creditcard.webapi.Mapping
{
    public static class DependencyInjection
    {
        public static void AddValidator(this IServiceCollection services)
        {
            services.AddTransient<IValidator<GetSingleConfigurationRequest>, ConfigurationValidator>();       
            services.AddValidatorsFromAssemblyContaining<ConfigurationValidator>();
            services.AddFluentValidationAutoValidation();
        }

        public static void AddHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks();
        }
    }
}
