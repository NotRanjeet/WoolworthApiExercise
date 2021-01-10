using System.Collections.Generic;

namespace Woolworth.Application.Trolley.Models
{
    public class TrolleySpecialDto
    {
        public IEnumerable<TrolleyQuantityDto> Quantities { get; set; }
        public decimal Total { get; set; }


    }
}
