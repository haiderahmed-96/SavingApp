using FluentValidation;
using SavingsApp.Models.Enums;

public class CreateTravelSavingValidator : AbstractValidator<CreateTravelSavingDto>
{
    public CreateTravelSavingValidator()
    {
        RuleFor(x => x.SavingGoalId)
            .GreaterThan(0).WithMessage("Valid saving goal ID is required");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required")
            .MaximumLength(50).WithMessage("Country must not exceed 50 characters");

        RuleFor(x => x.CurrencyType)
            .IsInEnum().WithMessage("Invalid currency type");

        RuleFor(x => x.EquivalentAmount)
            .GreaterThan(0).WithMessage("Equivalent amount must be greater than 0");
    }
}
