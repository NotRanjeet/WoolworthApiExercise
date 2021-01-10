using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Woolworth.Application.Common.Interfaces;
using Woolworth.Application.Common.Models;
using Woolworth.Application.Products.Models;

namespace Woolworth.Application.Products.Queries
{
    public class GetSortedProductsQuery: IRequest<IList<ProductDto>>
    {
        public SortOrder SortOption {get; private set;} 
        public GetSortedProductsQuery(SortOrder order)
        {
            SortOption = order;
        }
    }

    public class GetSortedProductsQueryHandler: IRequestHandler<GetSortedProductsQuery, IList<ProductDto>>
    {
        private readonly IProductsService _productsService;
        private readonly IRecommendationEngine _engine;
        public GetSortedProductsQueryHandler(IProductsService productsService, IRecommendationEngine engine)
        {
            _productsService = productsService;
            _engine = engine;
        }

        public async Task<IList<ProductDto>> Handle(GetSortedProductsQuery request, CancellationToken cancellationToken)
        {
            //in case it is sort of recommended then delegate it to recommendation engine instead.
            if (request.SortOption == SortOrder.Recommended)
            {
                return await _engine.GetRecommendedProductsByQuantity();
            }
            //For all other scenarios do the sorting in place.
            //If each sorting logic is lot more complex 
            //I will prefer creating query handler per sort order option
            var products = (await _productsService.GetProducts()).ToList();
            var isAscending = (int) request.SortOption > 0;
            var expression = BuildOrderBy(request.SortOption);
            var ordered =  isAscending ? products.OrderBy(expression) : products.OrderByDescending(expression);
            return ordered.ToList();
        }

        private Func<ProductDto, Object> BuildOrderBy(SortOrder sortBy){
            switch (sortBy)
            {
                case SortOrder.Low:
                case SortOrder.High:
                    return item => item.Price;

                case SortOrder.Ascending:
                case SortOrder.Descending:
                    return item => item.Name;
            }
            return item => item.Name;
        }
        

    }

    

    
}
