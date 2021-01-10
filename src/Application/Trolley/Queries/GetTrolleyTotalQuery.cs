using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Woolworth.Application.Common.Interfaces;
using Woolworth.Application.Trolley.Models;

namespace Woolworth.Application.Trolley.Queries
{
    public class GetTrolleyTotalQuery: IRequest<decimal>
    {
        public TrolleyDto Trolley {get;}

        public GetTrolleyTotalQuery(TrolleyDto trolley)
        {
            Trolley = trolley;
        }
    }

    public class GetTrolleyTotalQueryHandler : IRequestHandler<GetTrolleyTotalQuery, decimal>
    {
        private readonly IProductsService _productsService;

        public GetTrolleyTotalQueryHandler(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<decimal> Handle(GetTrolleyTotalQuery request, CancellationToken cancellationToken)
        {
            var total = await _productsService.GetTrolleyTotal(request.Trolley);
            return total;
        }
    }

}
