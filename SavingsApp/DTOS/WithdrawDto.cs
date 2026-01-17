using SavingsApp.Models.Enums;

public class WithdrawDto
{
    public int SavingGoalId { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
}
