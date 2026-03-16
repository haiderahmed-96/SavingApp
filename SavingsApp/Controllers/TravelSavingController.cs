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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var goal = await _context.SavingGoals
            .FirstOrDefaultAsync(g => g.Id == dto.SavingGoalId && g.UserId == dto.UserId);

        if (goal == null)
            return NotFound(new { error = "Saving goal not found" });

        var travel = new TravelSaving
        {
            SavingGoalId = dto.SavingGoalId,
            Country = dto.Country,
            CurrencyType = (CurrencyType)dto.CurrencyType,
            EquivalentAmount = dto.EquivalentAmount
        };

        try
        {
            _context.TravelSavings.Add(travel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { goalId = dto.SavingGoalId }, new { id = travel.Id, message = "Travel saving created successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while creating the travel saving", details = ex.Message });
        }
    }

    [HttpGet("{goalId}")]
    public async Task<IActionResult> Get(int goalId)
    {
        if (goalId <= 0)
            return BadRequest(new { error = "Invalid goal ID" });

        try
        {
            var travel = await _context.TravelSavings
                .FirstOrDefaultAsync(t => t.SavingGoalId == goalId);

            if (travel == null)
                return NotFound(new { error = "Travel saving not found for this goal" });

            return Ok(travel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the travel saving", details = ex.Message });
        }
    }

    [HttpPut("{goalId}")]
    public async Task<IActionResult> Update(int goalId, [FromBody] CreateTravelSavingDto dto)
    {
        if (goalId <= 0)
            return BadRequest(new { error = "Invalid goal ID" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var travel = await _context.TravelSavings
                .FirstOrDefaultAsync(t => t.SavingGoalId == goalId);

            if (travel == null)
                return NotFound(new { error = "Travel saving not found" });

            travel.Country = dto.Country;
            travel.CurrencyType = (CurrencyType)dto.CurrencyType;
            travel.EquivalentAmount = dto.EquivalentAmount;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Travel saving updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while updating the travel saving", details = ex.Message });
        }
    }

    [HttpDelete("{goalId}")]
    public async Task<IActionResult> Delete(int goalId)
    {
        if (goalId <= 0)
            return BadRequest(new { error = "Invalid goal ID" });

        try
        {
            var travel = await _context.TravelSavings
                .FirstOrDefaultAsync(t => t.SavingGoalId == goalId);

            if (travel == null)
                return NotFound(new { error = "Travel saving not found" });

            _context.TravelSavings.Remove(travel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Travel saving deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while deleting the travel saving", details = ex.Message });
        }
    }
}