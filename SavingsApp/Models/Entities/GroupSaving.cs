namespace SavingsApp.Models.Entities
{
    public class GroupSaving
    {
        public int Id { get; set; }

        public int SavingGoalId { get; set; }

        // Relations
        public SavingGoal SavingGoal { get; set; }
        public ICollection<GroupMember> GroupMembers { get; set; }
    }

}
