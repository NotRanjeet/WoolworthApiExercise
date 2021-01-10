using System.Collections.Generic;
using System.Threading.Tasks;
using Woolworth.Application.Products.Models;
using Woolworth.Application.Trolley.Models;

namespace Woolworth.Infrastructure.Api
{
    public interface IWoolworthApi
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<IEnumerable<HistoryDto>> GetShopperHistory();

        Task<decimal> GetTrolleyTotal(TrolleyDto trolley);
    }
}
