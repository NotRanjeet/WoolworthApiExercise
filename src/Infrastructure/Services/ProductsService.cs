using System.Collections.Generic;
using System.Threading.Tasks;
using Woolworth.Application.Common.Interfaces;
using Woolworth.Application.Products.Models;
using Woolworth.Application.Trolley.Models;
using Woolworth.Infrastructure.Api;

namespace Woolworth.Infrastructure.Services
{
    //In this example this service looks redundant
    //But i prefer to create services instead of exposing 3 part api 
    //Directly to the application we can project api response
    //We can orchestrate and aggregate in these services.
    public class ProductsService: IProductsService
    {
        private readonly IWoolworthApi _api;
        public ProductsService(IWoolworthApi api)
        {
            _api = api;
        }
        
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _api.GetProducts();
            return products;
        }

        public async Task<IEnumerable<HistoryDto>> GetShopperHistory()
        {
            var history = await _api.GetShopperHistory();
            return history;
        }

        public async Task<decimal> GetTrolleyTotal(TrolleyDto trolley)
        {
            var total = await _api.GetTrolleyTotal(trolley);
            return total;
        }
    }
}
