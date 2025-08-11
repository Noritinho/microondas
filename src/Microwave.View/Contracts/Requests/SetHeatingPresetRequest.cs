using System.ComponentModel.DataAnnotations;

namespace Microwave.Domain.Contracts.Requests;

public record SetHeatingPresetRequest
{
    [Required(ErrorMessage = "The identifier is required.")]
    public required string Identifier { get; init; }
}