using creditcard.webapi.Models.Request;
using FluentValidation;

namespace creditcard.webapi.Middlewares.Validations
{
    public class AddLogsInDBValidator : AbstractValidator<AddlogsInDBRequest>
    {
        public AddLogsInDBValidator()
        {
            RuleFor(e => e.ErrorMessage)
            .NotEmpty()
            .WithMessage("El mensaje de error no puede estar vacío.")
            .MaximumLength(4000)
            .WithMessage("El mensaje de error no puede exceder los 4000 caracteres.");

            RuleFor(e => e.ErrorNumber)
                .GreaterThan(0)
                .WithMessage("El número de error debe ser mayor que 0.");

            RuleFor(e => e.OriginatingComponent)
                .NotEmpty()
                .WithMessage("El componente de origen no puede estar vacío.")
                .MaximumLength(255)
                .WithMessage("El componente de origen no puede exceder los 255 caracteres.");

            RuleFor(e => e.AdditionalInfo)
                .MaximumLength(4000)
                .WithMessage("La información adicional no puede exceder los 4000 caracteres.");
        }
    }
}
