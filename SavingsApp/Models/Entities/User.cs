namespace SavingsApp.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        // Relations
        public ICollection<SavingGoal> SavingGoals { get; set; }
        public ICollection<SavingTransaction> SavingTransactions { get; set; }
        public ICollection<GroupMember> GroupMembers { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }

}
