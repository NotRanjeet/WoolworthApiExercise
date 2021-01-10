using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Woolworth.Application.Common.Interfaces;
using Woolworth.Application.Products.Models;

namespace Woolworth.Application.Products.Queries
{
    public class RecommendationEngine: IRecommendationEngine
    {
        private readonly IProductsService _productService;

        public RecommendationEngine(IProductsService productService)
        {
            _productService = productService;
        }


        private async Task<IList<ProductDto>> EvaluateRecommendation(bool isQuantityBases = true)
        {
            var recommendedProducts = new List<RecommendedProductDto>();
            var shopperHistory =await _productService.GetShopperHistory();
            var products =await _productService.GetProducts();
            var history = shopperHistory.SelectMany(i => i.Products).ToList();
            foreach (var product in products)
            {
                var rank = CalculateRank(history, product.Name);
                recommendedProducts.Add(new RecommendedProductDto(product, rank));
            }

            return recommendedProducts.OrderByDescending(i => i.Rank).Select(i => (ProductDto) i).ToList();
        }

        private decimal CalculateRank(IEnumerable<ProductDto> history, string productName, bool isQuantityBased = true)
        {
            var matches = history.Where(i => string.Compare(i.Name, productName, StringComparison.OrdinalIgnoreCase) == 0);
            return isQuantityBased ? matches.Sum(i => i.Quantity) : matches.Count();
        }


        public async Task<IList<ProductDto>> GetRecommendedProductsByQuantity()
        {
            return await EvaluateRecommendation();
        }

        public async Task<IList<ProductDto>> GetRecommendedProductsByOrders()
        {
            return await EvaluateRecommendation(false);
        }
    }
}
