using SavingsApp.Models.Enums;

public class AddSavingTransactionDto
{
    public int SavingGoalId { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public ContributionType ContributionType { get; set; }
}
