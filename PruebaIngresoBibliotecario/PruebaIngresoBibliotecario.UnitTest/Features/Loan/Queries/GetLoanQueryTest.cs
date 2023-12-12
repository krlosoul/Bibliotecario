namespace PruebaIngresoBibliotecario.UnitTest.Features.Loan.Queries
{
    using AutoMapper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using PruebaIngresoBibliotecario.Business.Common.Exceptions;
    using PruebaIngresoBibliotecario.Business.Contract.DataAccess;
    using PruebaIngresoBibliotecario.Business.Features.Loan.Queries;
    using PruebaIngresoBibliotecario.Core.Dtos;
    using PruebaIngresoBibliotecario.Core.Entities;
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    public class GetLoanQueryTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly GetLoanQueryHandler _getLoanQueryHandler;

        public GetLoanQueryTest()
        {
            _getLoanQueryHandler = new GetLoanQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task Handle_ExistingLoan_ReturnsLoanDto()
        {
            // Arrange
            var loanId = Guid.NewGuid(); 

            var existingLoan = new Loan { Id = loanId };

            _unitOfWorkMock.Setup(x => x.LoanRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Loan, bool>>>())).ReturnsAsync(existingLoan);

            var expectedLoanDto = new LoanDto();

            _mapperMock.Setup(x => x.Map<LoanDto>(existingLoan)).Returns(expectedLoanDto);

            var getLoanQuery = new GetLoanQuery { Id = loanId }; 

            // Act
            var result = await _getLoanQueryHandler.Handle(getLoanQuery, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLoanDto, result);
        }

        [TestMethod]
        public async Task Handle_NonExistingLoan_ThrowsNotFoundException()
        {
            // Arrange
            var loanId = Guid.NewGuid();

            _unitOfWorkMock.Setup(x => x.LoanRepository.FirstOrDefaultAsync(It.IsAny<Expression<Func<Loan, bool>>>())).ReturnsAsync((Loan)null);

            var getLoanQuery = new GetLoanQuery { Id = loanId }; 

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
            {
                await _getLoanQueryHandler.Handle(getLoanQuery, CancellationToken.None);
            });
        }
    }
}

