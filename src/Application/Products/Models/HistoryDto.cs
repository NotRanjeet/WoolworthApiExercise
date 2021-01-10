using System.Collections.Generic;

namespace Woolworth.Application.Products.Models
{
    public class HistoryDto
    {
        public int CustomerId { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
