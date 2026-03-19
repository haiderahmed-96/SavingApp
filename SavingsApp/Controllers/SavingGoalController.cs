using Microsoft.AspNetCore.Mvc;
using SavingsApp.Models.Entities;
using AutoMapper;
using SavingsApp.Exceptions;

[ApiController]
[Route("api/saving-goals")]
public class SavingGoalController : ControllerBase
{
    private readonly ISavingGoalService _savingGoalService;
    private readonly IMapper _mapper;

    public SavingGoalController(ISavingGoalService savingGoalService, IMapper mapper)
    {
        _savingGoalService = savingGoalService;
        _mapper = mapper;
    }

    /// <summary>
    /// Create a new saving goal
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSavingGoalDto dto)
    {
        var goal = _mapper.Map<SavingGoal>(dto);
        var id = await _savingGoalService.CreateSavingGoalAsync(goal);

        return CreatedAtAction(nameof(GetDetails), new { id }, new
        {
            Id = id,
            Message = "Saving goal created successfully"
        });
    }

    /// <summary>
    /// Get details of a specific saving goal
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(int id, [FromQuery] int userId)
    {
        if (userId <= 0)
            throw new BadRequestException("Valid user ID is required");

        var result = await _savingGoalService.GetGoalDetailsAsync(id, userId);

        if (result == null)
            throw new NotFoundException("Saving goal not found");

        return Ok(result);
    }

    /// <summary>
    /// Get all saving goals for a user
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetUserGoals([FromQuery] int userId)
    {
        if (userId <= 0)
            throw new BadRequestException("Valid user ID is required");

        var result = await _savingGoalService.GetUserGoalsAsync(userId);
        return Ok(result);
    }

    /// <summary>
    /// Update a saving goal
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSavingGoalDto dto)
    {
        if (id <= 0)
            throw new BadRequestException("Valid saving goal ID is required");

        await _savingGoalService.UpdateSavingGoalAsync(id, dto);
        return Ok(new { message = "Saving goal updated successfully" });
    }
}
