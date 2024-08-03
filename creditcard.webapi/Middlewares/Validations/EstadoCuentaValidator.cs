using creditcard.webapi.Models.Request;
using FluentValidation;
using System.Threading;

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
    //comands validations
    public class AddPagosValidator : AbstractValidator<AddpagoRequest>
    {
        public AddPagosValidator()
        {
            RuleFor(x => x.NumeroTarjeta)
                .NotEmpty().WithMessage("El numero de tarjeta es necesario")
                .Length(1, 16).WithMessage("No mas de 16 caracteres");
            RuleFor(x => x.Monto)
                .NotEmpty().WithMessage("El monto de pago es necesario");
        }
    }
    public class AddTransaccionValidator : AbstractValidator<AddTransaccionRequest>
    {
        public AddTransaccionValidator()
        {
            RuleFor(x => x.NumeroTarjeta)
                .NotEmpty().WithMessage("El numero de tarjeta es necesario")
                .Length(1, 16).WithMessage("No mas de 16 caracteres");
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("la Descripcion es necesaria");
            RuleFor(x => x.Monto)
                .NotEmpty().WithMessage("el monto es necesario");
            RuleFor(x => x.TipoTransaccion)
                .NotEmpty().WithMessage("la TipoTransaccion es necesaria");
            RuleFor(x => x.Categoria)
                .NotEmpty().WithMessage("la Categoria es necesaria");
        }
    }
}
