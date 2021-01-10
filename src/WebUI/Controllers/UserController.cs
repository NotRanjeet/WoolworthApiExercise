using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Woolworth.Application.User.Queries;

namespace Woolworth.WebUI.Controllers
{
    public class UserController: ApiController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDto), 200)]
        public async Task<UserDto> GetUser()
        {
            return await _mediator.Send(new GetUserQuery());
        }
    }
}
