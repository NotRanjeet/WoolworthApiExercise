using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Woolworth.Application.Common.Interfaces;

namespace Woolworth.Application.User.Queries
{
    public class GetUserQuery: IRequest<UserDto>
    {

    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserService _userService;
        public GetUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            //In real world it should be an API call
            //So will need async methods
            var user = _userService.GetCurrentUser();
            return Task.FromResult(user);
        }
    }
}
