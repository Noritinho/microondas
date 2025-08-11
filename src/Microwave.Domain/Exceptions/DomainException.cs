namespace Microwave.Domain.Exceptions;

public class DomainException : Exception
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public DomainException(string field, string message)
        : base(message)
    {
        Errors = new Dictionary<string, string[]>
        {
            { field, new[] { message } }
        };
    }
}