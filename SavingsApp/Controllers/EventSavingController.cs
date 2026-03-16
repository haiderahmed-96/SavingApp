using Microsoft.AspNetCore.Mvc;
using SavingsApp.Data;
using SavingsApp.Models.Entities;
using SavingsApp.Models.Enums;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/event-saving")]
public class EventSavingController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IEventSavingService _eventSavingService;

    public EventSavingController(AppDbContext context, IEventSavingService eventSavingService)
    {
        _context = context;
        _eventSavingService = eventSavingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventSavingDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var goal = await _context.SavingGoals
            .FirstOrDefaultAsync(g => g.Id == dto.SavingGoalId && g.UserId == dto.UserId);

        if (goal == null)
            return NotFound(new { error = "Saving goal not found" });

        var ev = new EventSaving
        {
            SavingGoalId = dto.SavingGoalId,
            EventDate = dto.EventDate,
            EventType = (EventType)dto.EventType
        };

        try
        {
            var id = await _eventSavingService.CreateEventSavingAsync(ev);
            return CreatedAtAction(nameof(Get), new { goalId = dto.SavingGoalId }, new { id = id, message = "Event saving created successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while creating the event saving", details = ex.Message });
        }
    }

    [HttpGet("{goalId}")]
    public async Task<IActionResult> Get(int goalId)
    {
        if (goalId <= 0)
            return BadRequest(new { error = "Invalid goal ID" });

        try
        {
            var eventSaving = await _eventSavingService.GetEventSavingAsync(goalId);

            if (eventSaving == null)
                return NotFound(new { error = "Event saving not found for this goal" });

            return Ok(eventSaving);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the event saving", details = ex.Message });
        }
    }

    [HttpGet("goal/{goalId}/all")]
    public async Task<IActionResult> GetAll(int goalId)
    {
        if (goalId <= 0)
            return BadRequest(new { error = "Invalid goal ID" });

        try
        {
            var events = await _eventSavingService.GetAllEventSavingsByGoalAsync(goalId);

            if (!events.Any())
                return NotFound(new { error = "No events found for this saving goal" });

            return Ok(events);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving events", details = ex.Message });
        }
    }

    [HttpPut("{goalId}")]
    public async Task<IActionResult> Update(int goalId, [FromBody] CreateEventSavingDto dto)
    {
        if (goalId <= 0)
            return BadRequest(new { error = "Invalid goal ID" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var ev = new EventSaving
            {
                EventDate = dto.EventDate,
                EventType = (EventType)dto.EventType
            };

            await _eventSavingService.UpdateEventSavingAsync(goalId, ev);
            return Ok(new { message = "Event saving updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while updating the event saving", details = ex.Message });
        }
    }

    [HttpDelete("{goalId}")]
    public async Task<IActionResult> Delete(int goalId)
    {
        if (goalId <= 0)
            return BadRequest(new { error = "Invalid goal ID" });

        try
        {
            await _eventSavingService.DeleteEventSavingAsync(goalId);
            return Ok(new { message = "Event saving deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while deleting the event saving", details = ex.Message });
        }
    }
}