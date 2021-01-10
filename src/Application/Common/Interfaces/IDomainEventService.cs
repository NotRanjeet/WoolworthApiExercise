using System.Threading.Tasks;
using Woolworth.Domain.Common;

namespace Woolworth.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
