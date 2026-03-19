using Microsoft.AspNetCore.Mvc;
using SavingsApp.Data;
using SavingsApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SavingsApp.Exceptions;

[ApiController]
[Route("api/event-saving")]
public class EventSavingController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IEventSavingService _eventSavingService;
    private readonly IMapper _mapper;

    public EventSavingController(AppDbContext context, IEventSavingService eventSavingService, IMapper mapper)
    {
        _context = context;
        _eventSavingService = eventSavingService;
        _mapper = mapper;
    }

    /// <summary>
    /// Create a new event saving
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventSavingDto dto)
    {
        var goal = await _context.SavingGoals
            .FirstOrDefaultAsync(g => g.Id == dto.SavingGoalId);

        if (goal == null)
            throw new NotFoundException("Saving goal not found");

        var ev = _mapper.Map<EventSaving>(dto);

        var id = await _eventSavingService.CreateEventSavingAsync(ev);
        return CreatedAtAction(nameof(Get), new { goalId = dto.SavingGoalId }, 
            new { id, message = "Event saving created successfully" });
    }

    /// <summary>
    /// Get event saving details
    /// </summary>
    [HttpGet("{goalId}")]
    public async Task<IActionResult> Get(int goalId)
    {
        if (goalId <= 0)
            throw new BadRequestException("Invalid goal ID");

        var eventSaving = await _eventSavingService.GetEventSavingAsync(goalId);

        if (eventSaving == null)
            throw new NotFoundException("Event saving not found for this goal");

        return Ok(eventSaving);
    }

    /// <summary>
    /// Get all events for a goal
    /// </summary>
    [HttpGet("goal/{goalId}/all")]
    public async Task<IActionResult> GetAll(int goalId)
    {
        if (goalId <= 0)
            throw new BadRequestException("Invalid goal ID");

        var events = await _eventSavingService.GetAllEventSavingsByGoalAsync(goalId);

        if (!events.Any())
            throw new NotFoundException("No events found for this saving goal");

        return Ok(events);
    }

    /// <summary>
    /// Update event saving
    /// </summary>
    [HttpPut("{goalId}")]
    public async Task<IActionResult> Update(int goalId, [FromBody] CreateEventSavingDto dto)
    {
        if (goalId <= 0)
            throw new BadRequestException("Invalid goal ID");

        var ev = _mapper.Map<EventSaving>(dto);
        await _eventSavingService.UpdateEventSavingAsync(goalId, ev);

        return Ok(new { message = "Event saving updated successfully" });
    }

    /// <summary>
    /// Delete event saving
    /// </summary>
    [HttpDelete("{goalId}")]
    public async Task<IActionResult> Delete(int goalId)
    {
        if (goalId <= 0)
            throw new BadRequestException("Invalid goal ID");

        await _eventSavingService.DeleteEventSavingAsync(goalId);
        return Ok(new { message = "Event saving deleted successfully" });
    }
}