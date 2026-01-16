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
        try
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
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Error = ex.Message
            });
        }
    }
}


