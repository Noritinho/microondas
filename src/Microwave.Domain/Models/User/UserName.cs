using Microsoft.EntityFrameworkCore;
using Microwave.Domain.Exceptions;

namespace Microwave.Domain.Models.User;

[Owned]
public class UserName
{
    public const string UserNameNull = "User name cannot be null or empty.";
    
    public UserName() { } // Entity Framework

    public string Value { get; private set; }

    private UserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new DomainException(nameof(UserName), UserNameNull);

        Value = userName;
    }
    
    public static UserName Create(string userName) => new(userName);
}