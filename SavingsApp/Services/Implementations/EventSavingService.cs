using SavingsApp.Data;
using SavingsApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class EventSavingService : IEventSavingService
{
    private readonly AppDbContext _context;

    public EventSavingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateEventSavingAsync(EventSaving eventSaving)
    {
        _context.EventSavings.Add(eventSaving);
        await _context.SaveChangesAsync();
        return eventSaving.Id;
    }

    public async Task<EventSaving> GetEventSavingAsync(int goalId)
    {
        return await _context.EventSavings
            .FirstOrDefaultAsync(e => e.SavingGoalId == goalId);
    }

    public async Task<List<EventSaving>> GetAllEventSavingsByGoalAsync(int goalId)
    {
        return await _context.EventSavings
            .Where(e => e.SavingGoalId == goalId)
            .ToListAsync();
    }

    public async Task UpdateEventSavingAsync(int goalId, EventSaving eventSaving)
    {
        var existing = await _context.EventSavings
            .FirstOrDefaultAsync(e => e.SavingGoalId == goalId);

        if (existing == null)
            throw new Exception("Event saving not found");

        existing.EventDate = eventSaving.EventDate;
        existing.EventType = eventSaving.EventType;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteEventSavingAsync(int goalId)
    {
        var eventSaving = await _context.EventSavings
            .FirstOrDefaultAsync(e => e.SavingGoalId == goalId);

        if (eventSaving == null)
            throw new Exception("Event saving not found");

        _context.EventSavings.Remove(eventSaving);
        await _context.SaveChangesAsync();
    }
}
