namespace SavingsApp.Models.Entities
{
    using SavingsApp.Models.Enums;

    public class EventSaving
    {
        public int Id { get; set; }

        public int SavingGoalId { get; set; }

        public DateTime EventDate { get; set; }

        public EventType EventType { get; set; }

        // Relations
        public SavingGoal SavingGoal { get; set; }
    }
}
