using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Woolworth.Application.Common.Models;
using Woolworth.Application.Products.Models;
using Woolworth.Application.Products.Queries;

namespace Woolworth.WebUI.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("sort")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        public async Task<IList<ProductDto>> GetProducts([FromQuery, Required] SortOrder sortOption)
        {
            return await _mediator.Send(new GetSortedProductsQuery(sortOption));
        }

    }
}
