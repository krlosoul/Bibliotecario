namespace PruebaIngresoBibliotecario.Business.Features.Loan.Commands
{
    using AutoMapper;
    using MediatR;
    using PruebaIngresoBibliotecario.Business.Common.Enums;
    using PruebaIngresoBibliotecario.Business.Common.Exceptions;
    using PruebaIngresoBibliotecario.Business.Contract.DataAccess;
    using PruebaIngresoBibliotecario.Business.Extensions;
    using PruebaIngresoBibliotecario.Core.Dtos;
    using PruebaIngresoBibliotecario.Core.Entities;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateLoanCommand : LoanCreateDto, IRequest<LoanCreateResponseDto> { }

    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, LoanCreateResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLoanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LoanCreateResponseDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            Loan give = await _unitOfWork.LoanRepository.FirstOrDefaultAsync(x => x.UserId == request.IdentificacionUsuario);
            if (give != null && request.TipoUsuario == (int)TiposUsuarioEnum.Invitado)
            {
                throw new BadRequestException($"El usuario con identificacion {request.IdentificacionUsuario} ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo");
            }
            give = _mapper.Map<Loan>(request);
            give.Id = Guid.NewGuid();
            give.DevolutionDate = CalculateDateExtension.CalcularFechaEntrega(request.TipoUsuario);
            await _unitOfWork.LoanRepository.InsertAsync(give);

            return _mapper.Map<LoanCreateResponseDto>(give);
        }
    }
}
