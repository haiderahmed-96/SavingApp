using SavingsApp.Models.Enums;

public class CreateSavingGoalDto
{
    public string GoalName { get; set; }
    public decimal TargetAmount { get; set; }
    public int DurationDays { get; set; }
    public SavingType SavingType { get; set; }
    public int UserId { get; set; }
}
