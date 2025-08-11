using System.ComponentModel.DataAnnotations;

namespace Microwave.Application.Contracts.Requests;

public record RegisterUserRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; init; }
    
    [Required(ErrorMessage = "Username is required")]
    public string Password { get; init; }
};