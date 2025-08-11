using System.Text.Json;

namespace Microwave.Application.Helpers;

public static class JsonHelper
{
    public static T DeserializeFromFileTo<T>(string path)
    {
        path = Path.Combine(AppContext.BaseDirectory, path);
        
        if (!File.Exists(path))
            throw new FileNotFoundException();
        
        var json = File.ReadAllText(path);
        
        var obj = JsonSerializer.Deserialize<T>(json) 
                  ?? throw new InvalidOperationException("O arquivo de presets está vazio ou inválido.");
        
        return obj;
    }
}