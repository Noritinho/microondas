using System.Text.RegularExpressions;

namespace Microwave.Domain.Extensions;

public static class TypeExtensions
{
    public static string ToTimeString(this TimeSpan time)
    {
        return time.TotalMinutes >= 1 ? $"{(int)time.TotalMinutes}:{time.Seconds:D2}" : $"{time.Seconds}";
    }

    public static bool IsHash256(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        input = input.Trim();
        
        var parts = input.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 2)
            return IsBase64WithLength(parts[0], 16) && IsBase64WithLength(parts[1], 32);
        
        return Regex.IsMatch(input, @"^[0-9a-fA-F]{64}$") || IsBase64WithLength(input, 32);
    }

    private static bool IsBase64WithLength(string s, int expectedBytes)
    {
        try
        {
            var normalized = s.Replace('-', '+').Replace('_', '/');
            switch (normalized.Length % 4)
            {
                case 2: normalized += "=="; break;
                case 3: normalized += "="; break;
            }

            var bytes = Convert.FromBase64String(normalized);
            return bytes.Length == expectedBytes;
        }
        catch
        {
            return false;
        }
    }
}