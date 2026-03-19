using FluentValidation;
using SavingsApp.Models.Enums;

public class AddSavingTransactionValidator : AbstractValidator<AddSavingTransactionDto>
{
    public AddSavingTransactionValidator()
    {
        RuleFor(x => x.SavingGoalId)
            .GreaterThan(0).WithMessage("Valid saving goal ID is required");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("Valid user ID is required");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.ContributionType)
            .IsInEnum().WithMessage("Invalid contribution type");
    }
}
