namespace PruebaIngresoBibliotecario.Business.Features.Loan.Queries
{
    using AutoMapper;
    using MediatR;
    using PruebaIngresoBibliotecario.Business.Common.Exceptions;
    using PruebaIngresoBibliotecario.Business.Contract.DataAccess;
    using PruebaIngresoBibliotecario.Core.Dtos;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetLoanQuery : GetLoanDto, IRequest<LoanDto> { }

    public class GetLoanQueryHandler : IRequestHandler<GetLoanQuery, LoanDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLoanQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LoanDto> Handle(GetLoanQuery request, CancellationToken cancellationToken)
        {
            var give = await _unitOfWork.LoanRepository.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new NotFoundException($"El prestamo con id {request.Id} no existe");
            LoanDto givDto = _mapper.Map<LoanDto>(give);

            return givDto;
        }
    }
}
