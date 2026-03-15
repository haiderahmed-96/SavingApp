using Microsoft.AspNetCore.Mvc;
using SavingsApp.Data;
using SavingsApp.Models.Entities;
using SavingsApp.Models.Enums;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/travel-saving")]
public class TravelSavingController : ControllerBase
{
    private readonly AppDbContext _context;

    public TravelSavingController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTravelSavingDto dto)
    {
        var goal = await _context.SavingGoals
            .FirstOrDefaultAsync(g => g.Id == dto.SavingGoalId && g.UserId == dto.UserId);

        if (goal == null)
            return BadRequest(new { error = "Saving goal not found" });

        var travel = new TravelSaving
        {
            SavingGoalId = dto.SavingGoalId,
            Country = dto.Country,
            CurrencyType = (CurrencyType)dto.CurrencyType,
            EquivalentAmount = dto.EquivalentAmount
        };

        _context.TravelSavings.Add(travel);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Travel saving created successfully" });
    }

    [HttpGet("{goalId}")]
    public async Task<IActionResult> Get(int goalId)
    {
        var travel = await _context.TravelSavings
            .FirstOrDefaultAsync(t => t.SavingGoalId == goalId);

        if (travel == null)
            return NotFound();

        return Ok(travel);
    }
}