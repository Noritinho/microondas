namespace Microwave.View.shared;

public class Server
{
    public  const string Uri = "http://localhost:5001/";
    public const string Api = Uri + "api/";
    public const string Hub = Uri + "microwaveHub";
    
    public static string Token { get; set; }
}