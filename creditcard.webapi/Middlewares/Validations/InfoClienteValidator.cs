using creditcard.webapi.Models.Request;
using FluentValidation;

namespace creditcard.webapi.Middlewares.Validations
{
    public class InfoClienteValidator
    {
    }

    public class GetPagosFromTarjetaValidator : AbstractValidator<GetPagosFromTarjetaRequest>
    {
        public GetPagosFromTarjetaValidator()
        {
            RuleFor(x => x.NumeroTarjeta)
                .NotEmpty().WithMessage("El numero de tarjeta es necesario")
                .Length(1, 16).WithMessage("No mas de 16 caracteres");
        }
    }
    public class GetTarjetasValidator : AbstractValidator<GetTarjetasRequest>
    {
        public GetTarjetasValidator()
        {
            RuleFor(x => x.IdCliente)
                .NotEmpty().WithMessage("El numero de cliente es necesario")
                .Must(IdCliente=> IdCliente > 4 && IdCliente < 13 ).WithMessage("El ID de cliente debe ser un entero mayor a 4 y menor a 13");
        }
    }
    public class GetTransaccionesValidator : AbstractValidator<GetTransaccionesRequest>
    {
        public GetTransaccionesValidator()
        {
            RuleFor(x => x.NumeroTarjeta)
                .NotEmpty().WithMessage("El numero de tarjeta es necesario")
                .Length(1, 16).WithMessage("No mas de 16 caracteres");
            RuleFor(x => x.FchInicio)
                .NotEmpty().WithMessage("La fecha inicio no puede estar vacia");
            RuleFor(x => x.NumeroTarjeta)
                .NotEmpty().WithMessage("La fecha inicio no puede estar vacia");
        }
    }
}
