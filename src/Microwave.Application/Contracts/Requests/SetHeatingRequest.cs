using System.ComponentModel.DataAnnotations;
using Microwave.Application.UseCases.Validations;
using Microwave.Domain.Models;

namespace Microwave.Application.Contracts.Requests;

public record SetHeatingRequest
{
    [HeatingDurationValidation]
    public int? HeatingDuration { get; init; }

    [HeatingPotencyValidation]
    public int? Potency { get; init; }
}