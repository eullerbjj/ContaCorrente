using System;
using System.Linq;
using ContaCorrente.Infrastructure.Helper;
using ContaCorrenteMS.Application.Commands.Transferencia;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContaCorrenteMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : Controller
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [RESTSecurityAttribute]
        [HttpPost("Transferencia")]
        public IActionResult Transferencia([FromBody] TransferenciaCommand command)
        {
            if (command == null)
                return BadRequest();

            var result = _mediator.Send(command).Result;

            if (result.DomainValidationMessages?.Count() > 0)
                return UnprocessableEntity(result);

            return Ok(result);

        }
    }
}
