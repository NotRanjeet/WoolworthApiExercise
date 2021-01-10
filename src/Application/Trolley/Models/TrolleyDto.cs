using System.Collections.Generic;

namespace Woolworth.Application.Trolley.Models
{
    public class TrolleyDto
    {
        public IEnumerable<TrolleyProductDto> Products { get; set; }
        public IEnumerable<TrolleySpecialDto> Specials { get; set; }
        public IEnumerable<TrolleyQuantityDto> Quantities { get; set; }
    }
}
