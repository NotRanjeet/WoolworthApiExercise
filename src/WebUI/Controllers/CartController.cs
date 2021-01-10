using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Woolworth.Application.Trolley.Models;
using Woolworth.Application.Trolley.Queries;

namespace Woolworth.WebUI.Controllers
{
    public class CartController: ApiController
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("trolleyTotal")]
        [ProducesResponseType(typeof(decimal), 200)]
        public async Task<decimal> CalculateTotal([FromBody] TrolleyDto trolley)
        {
            return await _mediator.Send(new GetTrolleyTotalQuery(trolley));
        }

    }
}
