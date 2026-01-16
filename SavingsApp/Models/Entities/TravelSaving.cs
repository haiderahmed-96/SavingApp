namespace SavingsApp.Models.Entities
{
   
    using SavingsApp.Models.Enums;

    public class TravelSaving
    {
        public int Id { get; set; }

        public int SavingGoalId { get; set; }

        public string Country { get; set; }

        public CurrencyType CurrencyType { get; set; }

        public decimal EquivalentAmount { get; set; }

        // Relations
        public SavingGoal SavingGoal { get; set; }
    }

}
