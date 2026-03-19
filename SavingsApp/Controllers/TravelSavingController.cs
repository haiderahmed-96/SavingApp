using Microsoft.AspNetCore.Mvc;
using SavingsApp.Data;
using SavingsApp.Models.Entities;
using SavingsApp.Models.Enums;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SavingsApp.Exceptions;

[ApiController]
[Route("api/travel-saving")]
public class TravelSavingController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TravelSavingController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Create a new travel saving
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTravelSavingDto dto)
    {
        var goal = await _context.SavingGoals
            .FirstOrDefaultAsync(g => g.Id == dto.SavingGoalId);

        if (goal == null)
            throw new NotFoundException("Saving goal not found");

        var travel = _mapper.Map<TravelSaving>(dto);

        _context.TravelSavings.Add(travel);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { goalId = dto.SavingGoalId }, 
            new { id = travel.Id, message = "Travel saving created successfully" });
    }

    /// <summary>
    /// Get travel saving details
    /// </summary>
    [HttpGet("{goalId}")]
    public async Task<IActionResult> Get(int goalId)
    {
        if (goalId <= 0)
            throw new BadRequestException("Invalid goal ID");

        var travel = await _context.TravelSavings
            .FirstOrDefaultAsync(t => t.SavingGoalId == goalId);

        if (travel == null)
            throw new NotFoundException("Travel saving not found for this goal");

        return Ok(travel);
    }

    /// <summary>
    /// Update travel saving
    /// </summary>
    [HttpPut("{goalId}")]
    public async Task<IActionResult> Update(int goalId, [FromBody] CreateTravelSavingDto dto)
    {
        if (goalId <= 0)
            throw new BadRequestException("Invalid goal ID");

        var travel = await _context.TravelSavings
            .FirstOrDefaultAsync(t => t.SavingGoalId == goalId);

        if (travel == null)
            throw new NotFoundException("Travel saving not found");

        travel.Country = dto.Country;
        travel.CurrencyType = (CurrencyType)dto.CurrencyType;
        travel.EquivalentAmount = dto.EquivalentAmount;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Travel saving updated successfully" });
    }

    /// <summary>
    /// Delete travel saving
    /// </summary>
    [HttpDelete("{goalId}")]
    public async Task<IActionResult> Delete(int goalId)
    {
        if (goalId <= 0)
            throw new BadRequestException("Invalid goal ID");

        var travel = await _context.TravelSavings
            .FirstOrDefaultAsync(t => t.SavingGoalId == goalId);

        if (travel == null)
            throw new NotFoundException("Travel saving not found");

        _context.TravelSavings.Remove(travel);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Travel saving deleted successfully" });
    }
}