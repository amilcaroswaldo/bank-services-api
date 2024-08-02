using creditcard.webapi.Models.Request;
using FluentValidation;
namespace creditcard.webapi.Middlewares.Validations
{


    public class ConfigurationValidator : AbstractValidator<GetSingleConfigurationRequest>
    {
        public ConfigurationValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El valor de nombre es requerido")
                .Length(1, 100).WithMessage("nombre debe estar 1 y 100 characters.");
        }
    }
}
