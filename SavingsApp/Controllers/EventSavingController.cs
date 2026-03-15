using Microsoft.AspNetCore.Mvc;
using SavingsApp.Data;
using SavingsApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/event-saving")]
public class EventSavingController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventSavingController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventSavingDto dto)
    {
        var goal = await _context.SavingGoals
            .FirstOrDefaultAsync(g => g.Id == dto.SavingGoalId && g.UserId == dto.UserId);

        if (goal == null)
            return BadRequest(new { error = "Saving goal not found" });

        var ev = new EventSaving
        {
            SavingGoalId = dto.SavingGoalId,
            EventDate = dto.EventDate,
            EventType = dto.EventType
        };

        _context.EventSavings.Add(ev);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Event saving created successfully" });
    }

    [HttpGet("{goalId}")]
    public async Task<IActionResult> Get(int goalId)
    {
        var ev = await _context.EventSavings
            .FirstOrDefaultAsync(e => e.SavingGoalId == goalId);

        if (ev == null)
            return NotFound();

        return Ok(ev);
    }
}