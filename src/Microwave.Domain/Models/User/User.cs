namespace Microwave.Domain.Models.User;

public class User
{
    public int Id { get; private set; }
    public UserName Name { get; private set; }
    public UserPassword Password { get; private set; }

    public static User Create(string username, string password)
    {
        return new User
        {
            Name = UserName.Create(username),
            Password = UserPassword.Create(password)
        };
    }
}