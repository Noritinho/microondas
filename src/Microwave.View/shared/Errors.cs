using System.Text.Json;

namespace Microwave.View.shared;

public class ValidationProblemDetails
{
    public string Title { get; set; }
    public int Status { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
}

public class ServerErrorResponse
{
    public string Error { get; set; }
}

public static class ErrorHandler
{
    public static async Task<string> HandleApiError(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            var validationErrors = JsonSerializer.Deserialize<ValidationProblemDetails>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (validationErrors?.Errors != null)
            {
                return string.Join("; ", validationErrors.Errors.SelectMany(e => e.Value));
            }
            return validationErrors?.Title ?? "Erro de validação no servidor.";
        }
        else if ((int)response.StatusCode >= 500)
        {
            var serverError = JsonSerializer.Deserialize<ServerErrorResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return serverError?.Error ?? "Erro inesperado no servidor.";
        }

        return "Erro desconhecido.";
    }
}

