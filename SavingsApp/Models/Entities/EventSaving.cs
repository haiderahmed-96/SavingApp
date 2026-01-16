namespace SavingsApp.Models.Entities
{
    public class EventSaving
    {
        public int Id { get; set; }

        public int SavingGoalId { get; set; }

        public DateTime EventDate { get; set; }

        public string EventType { get; set; }

        // Relations
        public SavingGoal SavingGoal { get; set; }
    }

}
