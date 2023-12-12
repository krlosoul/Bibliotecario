namespace PruebaIngresoBibliotecario.UnitTest.Features.Loan.Commands
{
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using PruebaIngresoBibliotecario.Business.Common.Enums;
    using PruebaIngresoBibliotecario.Business.Common.Exceptions;
    using PruebaIngresoBibliotecario.Business.Contract.DataAccess;
    using PruebaIngresoBibliotecario.Business.Features.Loan.Commands;
    using PruebaIngresoBibliotecario.Core.Dtos;
    using PruebaIngresoBibliotecario.Core.Entities;
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    public class CreateLoanCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly CreateLoanCommandHandler _createLoanCommandHandler;

        public CreateLoanCommandHandlerTest()
        {
            _createLoanCommandHandler = new CreateLoanCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task Handle_WhenValidRequest_ReturnsLoanCreateResponseDto()
        {
            // Arrange
            var createLoanCommand = new CreateLoanCommand
            {
                IdentificacionUsuario = "abc123",
                TipoUsuario = (int)TiposUsuarioEnum.Afiliado,
                Isbn = new Guid(),
                
            };
            var expectedLoanCreateResponse = new LoanCreateResponseDto();

            _unitOfWorkMock.Setup(x => x.LoanRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Loan, bool>>>())).ReturnsAsync((new Loan())); 

            _mapperMock.Setup(x => x.Map<Loan>(createLoanCommand)).Returns(new Loan());

            _unitOfWorkMock.Setup(x => x.LoanRepository.InsertAsync(It.IsAny<Loan>())).ReturnsAsync(true);

            _mapperMock.Setup(x => x.Map<LoanCreateResponseDto>(It.IsAny<Loan>())).Returns(expectedLoanCreateResponse);

            // Act
            var result = await _createLoanCommandHandler.Handle(createLoanCommand, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLoanCreateResponse, result);
        }

        [TestMethod]
        public async Task Handle_UserAlreadyHasLoan_ThrowsBadRequestException()
        {
            // Arrange
            var createLoanCommand = new CreateLoanCommand
            {
                IdentificacionUsuario = "user123",
                TipoUsuario = (int)TiposUsuarioEnum.Invitado,
                Isbn = new Guid(),
            };

            var existingLoan = new Loan(); 
            _unitOfWorkMock.Setup(x => x.LoanRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Loan, bool>>>())).ReturnsAsync((new Loan()));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<BadRequestException>(async () =>
            {
                await _createLoanCommandHandler.Handle(createLoanCommand, CancellationToken.None);
            });
        }
    }
}
