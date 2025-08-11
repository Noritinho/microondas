using System.Security.Cryptography;
using System.Text;
using Microwave.Domain.Exceptions;

namespace Microwave.Domain.Security;

public static class Cryptograph
{
    public const string PasswordNull = "Password is Null";
    
    public static string Encrypt(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new DomainException(nameof(Cryptograph), PasswordNull);
        
        var saltBytes = RandomNumberGenerator.GetBytes(16);
        var salt = Convert.ToBase64String(saltBytes);

        var combined = Encoding.UTF8.GetBytes(password + salt);
        var hash = SHA256.HashData(combined);
        var hashBase64 = Convert.ToBase64String(hash);

        return $"{salt}.{hashBase64}";
    }

    public static bool Verify(string password, string storedHash)
    {
        if(string.IsNullOrWhiteSpace(storedHash) || string.IsNullOrWhiteSpace(password))
            throw new DomainException(nameof(Cryptograph), PasswordNull);
        
        var parts = storedHash.Split('.');
        if (parts.Length != 2)
            return false;

        var salt = parts[0];
        var hash = parts[1];

        var combined = Encoding.UTF8.GetBytes(password + salt);
        var computedHash = Convert.ToBase64String(SHA256.HashData(combined));

        return computedHash == hash;
    }
}