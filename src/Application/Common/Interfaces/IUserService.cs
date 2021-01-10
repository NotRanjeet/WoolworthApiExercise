namespace Woolworth.Application.Common.Interfaces
{
    public interface IUserService
    {
        public User.Queries.UserDto GetCurrentUser();
    }
}
