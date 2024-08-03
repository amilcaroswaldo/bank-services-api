using creditcard.webapi.Models.Request;
using FluentValidation;

namespace creditcard.webapi.Middlewares.Validations
{
    public class EstadoCuentaValidator : AbstractValidator<GetEstadoCuentaRequest>
    {
        public EstadoCuentaValidator()
        {
            RuleFor(x => x.Numero_Tarjeta)
                .NotEmpty().WithMessage("El numero de tarjeta es necesario")
                .Length(1, 16).WithMessage("No mas de 16 caracteres");
        }
    }
    public class CuotaMinimaValidator : AbstractValidator<CuotaMinimaRequest>
    {
        public CuotaMinimaValidator()
        {
            RuleFor(x => x.NumeroTarjeta)
                .NotEmpty().WithMessage("El numero de tarjeta es necesario")
                .Length(1, 16).WithMessage("No mas de 16 caracteres");
        }
    }
    public class InteresBonificableValidator : AbstractValidator<InteresBonificableRequest>
    {
        public InteresBonificableValidator()
        {
            RuleFor(x => x.NumeroTarjeta)
                .NotEmpty().WithMessage("El numero de tarjeta es necesario")
                .Length(1, 16).WithMessage("No mas de 16 caracteres");
        }
    }
    public class MontoContadoConInteresesValidator : AbstractValidator<MontoContadoConInteresesRequest>
    {
        public MontoContadoConInteresesValidator()
        {
            RuleFor(x => x.NumeroTarjeta)
                .NotEmpty().WithMessage("El numero de tarjeta es necesario")
                .Length(1, 16).WithMessage("No mas de 16 caracteres");
        }
    }
}
