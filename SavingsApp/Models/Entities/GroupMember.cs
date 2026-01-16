namespace SavingsApp.Models.Entities
{
    using SavingsApp.Models.Enums;

    public class GroupMember
    {
        public int Id { get; set; }

        public int GroupSavingId { get; set; }

        public int UserId { get; set; }

        public GroupRole Role { get; set; }

        // Relations
        public GroupSaving GroupSaving { get; set; }
        public User User { get; set; }
    }

}
