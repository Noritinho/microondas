using System.ComponentModel.DataAnnotations;
using Microwave.Domain.Models.Heating;

namespace Microwave.Application.UseCases.Validations;

public class HeatingDurationValidationAttribute : ValidationAttribute
{
    /// <summary>
    /// Caso o valor não seja do tipo <see cref="byte"/>, a validação passa,
    /// pois sera tratado no domínio.
    /// </summary>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is not byte)
            return ValidationResult.Success;
        
        if(TimeSpan.FromSeconds((byte)value) > TimeSpan.FromSeconds(CustomHeatingDuration.MaxDurationInSeconds) ||
           TimeSpan.FromSeconds((byte)value) < TimeSpan.FromSeconds(CustomHeatingDuration.MinDurationInSeconds))
            return new ValidationResult($"Heat time must be between {CustomHeatingDuration.MinDurationInSeconds} and {CustomHeatingDuration.MaxDurationInSeconds} seconds.");
        
        return ValidationResult.Success;
    }
}

public class HeatingPotencyValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is not byte)
            return ValidationResult.Success;
        
        if((byte?)value > IHEatingPotency.MaxPotency || (byte?)value < IHEatingPotency.MinPotency)
            return new ValidationResult($"Heat time must be between {IHEatingPotency.MinPotency} and {IHEatingPotency.MaxPotency} seconds.");
        
        return ValidationResult.Success;
    }
}