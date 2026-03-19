using FluentValidation;
using SavingsApp.Models.Enums;

public class CreateSavingGoalValidator : AbstractValidator<CreateSavingGoalDto>
{
    public CreateSavingGoalValidator()
    {
        RuleFor(x => x.GoalName)
            .NotEmpty().WithMessage("Goal name is required")
            .MaximumLength(100).WithMessage("Goal name must not exceed 100 characters");

        RuleFor(x => x.TargetAmount)
            .GreaterThan(0).WithMessage("Target amount must be greater than 0");

        RuleFor(x => x.DurationDays)
            .GreaterThan(0).WithMessage("Duration must be greater than 0");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("Valid user ID is required");

        RuleFor(x => x.SavingType)
            .IsInEnum().WithMessage("Invalid saving type");
    }
}
