using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Woolworth.Application.Common.Interfaces;
using Woolworth.Application.Products.Models;

namespace Woolworth.Application.Products.Queries
{
    //Default query that does not do any sort and stuff
    public class GetProductsQuery: IRequest<IEnumerable<ProductDto>>
    {
        
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductsService _productsService;
        public GetProductsQueryHandler(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var apiResults =await _productsService.GetProducts();
            return apiResults;
        }
    }
}
