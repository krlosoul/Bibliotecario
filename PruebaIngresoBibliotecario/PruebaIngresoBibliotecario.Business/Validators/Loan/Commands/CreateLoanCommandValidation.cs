namespace PruebaIngresoBibliotecario.Business.Validators.Loan.Commands
{
    using FluentValidation;
    using PruebaIngresoBibliotecario.Business.Features.Loan.Commands;

    public class CreateLoanCommandValidation : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidation()
        {
            RuleFor(r => r.TipoUsuario)
                .NotEmpty()
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("El 'TipoUsuario' es obligatorio.");

            RuleFor(r => r.TipoUsuario)
                .InclusiveBetween(1, 3)
                .WithErrorCode("400")
                .WithMessage("El 'TipoUsuario' debe estar entre 1 y 3.");

            RuleFor(r => r.IdentificacionUsuario)
                .NotEmpty()
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("El 'IdentificacionUsuario' es obligatorio.");

            RuleFor(r => r.IdentificacionUsuario)
               .MaximumLength(10)
               .WithErrorCode("400")
               .WithMessage("La Longitud maxima del campo 'IdentificacionUsuario' es 10.");

            RuleFor(r => r.Isbn)
                .NotEmpty()
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("El 'Isbn' es obligatorio.");
        }
    }
}
