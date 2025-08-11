using Microsoft.EntityFrameworkCore;
using Microwave.Domain.Exceptions;
using Microwave.Domain.Extensions;

namespace Microwave.Domain.Models.User;

[Owned]
public class UserPassword
{
    public const string PasswordNull = "User name cannot be null or empty.";
    public const string InvalidHash = "Invalid hash 256";
    
    public UserPassword() { } // Entity Framework

    public string Value { get; private set; }

    private UserPassword(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new DomainException(nameof(UserPassword), PasswordNull);
        
        if (!hash.IsHash256())
            throw new DomainException(nameof(UserPassword), InvalidHash);
        
        Value = hash;
    }
    
    public static UserPassword Create(string hash) => new(hash);
}