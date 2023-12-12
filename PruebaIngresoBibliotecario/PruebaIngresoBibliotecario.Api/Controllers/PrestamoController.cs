namespace PruebaIngresoBibliotecario.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using PruebaIngresoBibliotecario.Business.Features.Loan.Commands;
    using PruebaIngresoBibliotecario.Business.Features.Loan.Queries;
    using PruebaIngresoBibliotecario.Core.Dtos;
    using System;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrestamoController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Create Give.
        /// </summary>
        /// <param name="createGiveCommand">The CreateGiveDto.</param>
        /// <returns>&lt;CreateGiveResponseDto&gt;</returns>
        [HttpPost]
        public async Task<LoanCreateResponseDto> CreateLoad([FromBody] CreateLoanCommand createGiveCommand) => await _mediator.Send(createGiveCommand);

        /// <summary>
        /// Get Give.
        /// </summary>
        /// <param name="GetGiveQuery">The GetGiveDto.</param>
        /// <returns>&lt;GiveDto&gt;</returns>
        [HttpGet("{id}")]
        public Task<LoanDto> GetLoad(Guid id)
        {
            GetLoanQuery getGiveQuery = new GetLoanQuery { Id = id };

            return _mediator.Send(getGiveQuery);
        }
    }
}
