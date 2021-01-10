using System.Collections.Generic;
using System.Threading.Tasks;
using Woolworth.Application.Products.Models;

namespace Woolworth.Application.Common.Interfaces
{
    public interface IRecommendationEngine
    {
        Task<IList<ProductDto>> GetRecommendedProductsByQuantity();
        Task<IList<ProductDto>> GetRecommendedProductsByOrders();

    }
}
