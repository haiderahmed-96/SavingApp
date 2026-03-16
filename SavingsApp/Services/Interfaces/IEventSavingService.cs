using SavingsApp.Models.Entities;

public interface IEventSavingService
{
    Task<int> CreateEventSavingAsync(EventSaving eventSaving);
    Task<EventSaving> GetEventSavingAsync(int goalId);
    Task<List<EventSaving>> GetAllEventSavingsByGoalAsync(int goalId);
    Task UpdateEventSavingAsync(int goalId, EventSaving eventSaving);
    Task DeleteEventSavingAsync(int goalId);
}
