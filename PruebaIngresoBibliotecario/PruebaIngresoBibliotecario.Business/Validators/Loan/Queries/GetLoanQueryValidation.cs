namespace PruebaIngresoBibliotecario.Business.Validators.Loan.Queries
{
    using FluentValidation;
    using PruebaIngresoBibliotecario.Business.Features.Loan.Queries;

    public class GetLoanQueryValidation : AbstractValidator<GetLoanQuery>
    {
        public GetLoanQueryValidation()
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .NotNull()
                .WithErrorCode("400")
                .WithMessage("El 'Id' es obligatorio.");
        }
    }
}
