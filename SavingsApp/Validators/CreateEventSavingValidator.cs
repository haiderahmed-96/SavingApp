using FluentValidation;
using SavingsApp.Models.Enums;

public class CreateEventSavingValidator : AbstractValidator<CreateEventSavingDto>
{
    public CreateEventSavingValidator()
    {
        RuleFor(x => x.SavingGoalId)
            .GreaterThan(0).WithMessage("Valid saving goal ID is required");

        RuleFor(x => x.EventDate)
            .GreaterThan(DateTime.Now).WithMessage("Event date must be in the future");

        RuleFor(x => x.EventType)
            .IsInEnum().WithMessage("Invalid event type");
    }
}
