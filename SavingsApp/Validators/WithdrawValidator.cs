using FluentValidation;

public class WithdrawValidator : AbstractValidator<WithdrawDto>
{
    public WithdrawValidator()
    {
        RuleFor(x => x.SavingGoalId)
            .GreaterThan(0).WithMessage("Valid saving goal ID is required");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("Valid user ID is required");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Withdraw amount must be greater than 0");
    }
}
