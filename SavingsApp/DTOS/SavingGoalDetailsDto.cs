using SavingsApp.Models.Enums;

public class SavingGoalDetailsDto
{
    public int Id { get; set; }
    public string GoalName { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public int DurationDays { get; set; }
    public SavingType SavingType { get; set; }
    public SavingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<SavingTransactionDto> Transactions { get; set; } = new();
}

public class SavingTransactionDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int ContributionType { get; set; } // أو  enum  
    public DateTime CreatedAt { get; set; }
}
