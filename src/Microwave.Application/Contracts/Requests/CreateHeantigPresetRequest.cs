using System.ComponentModel.DataAnnotations;

namespace Microwave.Application.Contracts.Requests;

public record CreateHeantigPresetRequest
{
    [Required(ErrorMessage = "Identifier is required.")]
    [DeniedValues(".", ErrorMessage = "Identifier is invalid.")]
    public string Identifier { get; init; }
    
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; init; }

    [Required(ErrorMessage = "Food is required.")]
    public string Food { get; init; }
    
    [Required(ErrorMessage = "Duration is required.")]
    public int Duration { get; init; }
    
    [Required(ErrorMessage = "Potency is required.")]
    public byte Potency { get; init; }
    
    public string? Instructions { get; init; }
}