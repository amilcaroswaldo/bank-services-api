using creditcard.webapi.Models.Request;
using FluentValidation;

namespace creditcard.webapi.Middlewares.Validations
{
    public class HealthCheckValidator : AbstractValidator<HealthCheckApiRequest>
    {
        public HealthCheckValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("La URL es necesaria para verificar la salud del servicio");
        }
    }
}
