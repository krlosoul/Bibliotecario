namespace PruebaIngresoBibliotecario.UnitTest.Controllers
{
    using MediatR;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using PruebaIngresoBibliotecario.Api.Controllers;
    using PruebaIngresoBibliotecario.Business.Features.Loan.Commands;
    using PruebaIngresoBibliotecario.Business.Features.Loan.Queries;
    using PruebaIngresoBibliotecario.Core.Dtos;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    public class PrestamoControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private readonly PrestamoController _prestamoController;

        public PrestamoControllerTest()
        {
            _prestamoController = new PrestamoController(_mediatorMock.Object);
        }

        [TestMethod]
        public async Task CreateLoad_ValidData_ReturnsLoanCreateResponseDto()
        {
            // Arrange
            var createLoanCommand = new CreateLoanCommand();
            var expectedResponse = new LoanCreateResponseDto(); 

            _mediatorMock.Setup(x => x.Send(createLoanCommand, CancellationToken.None)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _prestamoController.CreateLoad(createLoanCommand);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse, result);
        }

        [TestMethod]
        public async Task GetLoad_ExistingId_ReturnsLoanDto()
        {
            // Arrange
            var loanId = Guid.NewGuid();

            var expectedLoanDto = new LoanDto();

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetLoanQuery>(), CancellationToken.None)).ReturnsAsync(expectedLoanDto);

            // Act
            var result = await _prestamoController.GetLoad(loanId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLoanDto, result);
        }
    }
}

