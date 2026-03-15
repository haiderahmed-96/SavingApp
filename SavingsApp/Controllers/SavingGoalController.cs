using Microsoft.AspNetCore.Mvc;
using SavingsApp.Models.Entities;

[ApiController]
[Route("api/saving-goals")]
public class SavingGoalController : ControllerBase
{
    private readonly ISavingGoalService _savingGoalService;

    public SavingGoalController(ISavingGoalService savingGoalService)
    {
        _savingGoalService = savingGoalService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSavingGoalDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid request");

        var goal = new SavingGoal
        {
            UserId = dto.UserId,
            GoalName = dto.GoalName,
            TargetAmount = dto.TargetAmount,
            DurationDays = dto.DurationDays,
            SavingType = dto.SavingType
        };

        var id = await _savingGoalService.CreateSavingGoalAsync(goal);

        return Ok(new
        {
            Id = id,
            Message = "Saving goal created successfully"
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(int id, [FromQuery] int userId)
    {
        var result = await _savingGoalService.GetGoalDetailsAsync(id, userId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserGoals([FromQuery] int userId)
    {
        var result = await _savingGoalService.GetUserGoalsAsync(userId);
        return Ok(result);
    }
     [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateSavingGoalDto dto)
    {
        await _savingGoalService.UpdateSavingGoalAsync(id, dto);
        return Ok(new { message = "Saving goal updated successfully" });
    }

}
