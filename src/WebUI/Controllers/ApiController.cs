using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Woolworth.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    public abstract class ApiController : ControllerBase
    {
        //Some basic shared controller 
    }
}
