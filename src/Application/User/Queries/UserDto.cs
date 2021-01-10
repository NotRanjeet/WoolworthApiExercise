namespace Woolworth.Application.User.Queries
{
    public class UserDto
    {
        public string Name { get;set;}
        public string Token { get;set;}

        public UserDto(string name, string token)
        {
            Name = name;
            Token = token;
        }
    }
}
