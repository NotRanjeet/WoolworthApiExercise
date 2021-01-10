using System.Collections.Generic;
using System.Threading.Tasks;
using Woolworth.Application.Products.Models;
using Woolworth.Application.Trolley.Models;

namespace Woolworth.Application.Common.Interfaces
{
    public interface IProductsService
    {
        public Task<IEnumerable<ProductDto>> GetProducts();

        public Task<IEnumerable<HistoryDto>> GetShopperHistory();

        public Task<decimal> GetTrolleyTotal(TrolleyDto trolley);
    }
}
