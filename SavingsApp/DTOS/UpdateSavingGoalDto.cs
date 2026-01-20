public class UpdateSavingGoalDto
{
    public int UserId { get; set; }
    public string GoalName { get; set; }
    public decimal TargetAmount { get; set; }
    public int DurationDays { get; set; }
}
