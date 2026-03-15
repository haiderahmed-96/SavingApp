public class CreateTravelSavingDto
{
    public int SavingGoalId { get; set; }
    public int UserId { get; set; }
    public string Country { get; set; }
    public int CurrencyType { get; set; }
    public decimal EquivalentAmount { get; set; }
}