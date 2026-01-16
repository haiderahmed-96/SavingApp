namespace SavingsApp.Models.Entities
{
  
    using SavingsApp.Models.Enums;

    public class SavingTransaction
    {
        public int Id { get; set; }

        public int SavingGoalId { get; set; }

        public int UserId { get; set; }

        public decimal Amount { get; set; }

        public ContributionType ContributionType { get; set; }

        public DateTime CreatedAt { get; set; }

        // Relations
        public SavingGoal SavingGoal { get; set; }
        public User User { get; set; }
    }

}
