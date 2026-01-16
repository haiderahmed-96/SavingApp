namespace SavingsApp.Models.Entities
{
    
    using SavingsApp.Models.Enums;

    public class SavingGoal
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string GoalName { get; set; }

        public decimal TargetAmount { get; set; }

        public decimal CurrentAmount { get; set; }

        public int DurationDays { get; set; }

        public SavingType SavingType { get; set; }

        public SavingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        // Relations
        public User User { get; set; }
        public ICollection<SavingTransaction> SavingTransactions { get; set; }

        public EventSaving EventSaving { get; set; }
        public TravelSaving TravelSaving { get; set; }
        public GroupSaving GroupSaving { get; set; }
    }


}
