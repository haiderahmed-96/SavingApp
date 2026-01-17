using SavingsApp.Models.Enums;

public class SavingGoalListItemDto
{
    public int Id { get; set; }
    public string GoalName { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public int DurationDays { get; set; }
    public SavingType SavingType { get; set; }
    public SavingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    // نسبة الإنجاز %
    public decimal ProgressPercent { get; set; }
}
