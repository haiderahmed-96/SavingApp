using FluentValidation;

public class UpdateSavingGoalValidator : AbstractValidator<UpdateSavingGoalDto>
{
    public UpdateSavingGoalValidator()
    {
        RuleFor(x => x.GoalName)
            .NotEmpty().WithMessage("Goal name is required")
            .MaximumLength(100).WithMessage("Goal name must not exceed 100 characters")
            .When(x => !string.IsNullOrEmpty(x.GoalName));

        RuleFor(x => x.TargetAmount)
            .GreaterThan(0).WithMessage("Target amount must be greater than 0")
            .When(x => x.TargetAmount > 0);

        RuleFor(x => x.DurationDays)
            .GreaterThan(0).WithMessage("Duration must be greater than 0")
            .When(x => x.DurationDays > 0);
    }
}
