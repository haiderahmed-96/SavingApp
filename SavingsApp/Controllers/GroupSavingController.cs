using Microsoft.AspNetCore.Mvc;
using SavingsApp.Data;
using SavingsApp.Models.Entities;
using SavingsApp.Models.Enums;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/group-saving")]
public class GroupSavingController : ControllerBase
{
    private readonly AppDbContext _context;

    public GroupSavingController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGroupSavingDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var goal = await _context.SavingGoals
            .FirstOrDefaultAsync(g => g.Id == dto.SavingGoalId && g.UserId == dto.UserId);

        if (goal == null)
            return NotFound(new { error = "Saving goal not found" });

        // Check if group saving already exists for this goal
        var existingGroup = await _context.GroupSavings
            .FirstOrDefaultAsync(g => g.SavingGoalId == dto.SavingGoalId);

        if (existingGroup != null)
            return BadRequest(new { error = "Group saving already exists for this goal" });

        var groupSaving = new GroupSaving
        {
            SavingGoalId = dto.SavingGoalId,
            GroupMembers = new List<GroupMember>
            {
                new GroupMember
                {
                    UserId = dto.UserId,
                    Role = GroupRole.Owner
                }
            }
        };

        try
        {
            _context.GroupSavings.Add(groupSaving);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { goalId = dto.SavingGoalId }, new { id = groupSaving.Id, message = "Group saving created successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while creating the group saving", details = ex.Message });
        }
    }

    [HttpGet("{goalId}")]
    public async Task<IActionResult> Get(int goalId)
    {
        if (goalId <= 0)
            return BadRequest(new { error = "Invalid goal ID" });

        try
        {
            var groupSaving = await _context.GroupSavings
                .Include(g => g.GroupMembers)
                .FirstOrDefaultAsync(g => g.SavingGoalId == goalId);

            if (groupSaving == null)
                return NotFound(new { error = "Group saving not found for this goal" });

            return Ok(groupSaving);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while retrieving the group saving", details = ex.Message });
        }
    }

    [HttpPost("{goalId}/members")]
    public async Task<IActionResult> AddMember(int goalId, [FromBody] AddGroupMemberDto dto)
    {
        if (goalId <= 0)
            return BadRequest(new { error = "Invalid goal ID" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var groupSaving = await _context.GroupSavings
                .FirstOrDefaultAsync(g => g.SavingGoalId == goalId);

            if (groupSaving == null)
                return NotFound(new { error = "Group saving not found" });

            // Check if user already a member
            var existingMember = await _context.GroupMembers
                .FirstOrDefaultAsync(m => m.GroupSavingId == groupSaving.Id && m.UserId == dto.UserId);

            if (existingMember != null)
                return BadRequest(new { error = "User is already a member of this group" });

            var member = new GroupMember
            {
                GroupSavingId = groupSaving.Id,
                UserId = dto.UserId,
                Role = GroupRole.Member
            };

            _context.GroupMembers.Add(member);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Member added successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while adding the member", details = ex.Message });
        }
    }

    [HttpDelete("{goalId}/members/{userId}")]
    public async Task<IActionResult> RemoveMember(int goalId, int userId)
    {
        if (goalId <= 0 || userId <= 0)
            return BadRequest(new { error = "Invalid goal ID or user ID" });

        try
        {
            var groupSaving = await _context.GroupSavings
                .FirstOrDefaultAsync(g => g.SavingGoalId == goalId);

            if (groupSaving == null)
                return NotFound(new { error = "Group saving not found" });

            var member = await _context.GroupMembers
                .FirstOrDefaultAsync(m => m.GroupSavingId == groupSaving.Id && m.UserId == userId);

            if (member == null)
                return NotFound(new { error = "Member not found" });

            if (member.Role == GroupRole.Owner)
                return BadRequest(new { error = "Cannot remove the owner of the group" });

            _context.GroupMembers.Remove(member);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Member removed successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while removing the member", details = ex.Message });
        }
    }

    [HttpDelete("{goalId}")]
    public async Task<IActionResult> Delete(int goalId)
    {
        if (goalId <= 0)
            return BadRequest(new { error = "Invalid goal ID" });

        try
        {
            var groupSaving = await _context.GroupSavings
                .FirstOrDefaultAsync(g => g.SavingGoalId == goalId);

            if (groupSaving == null)
                return NotFound(new { error = "Group saving not found" });

            _context.GroupSavings.Remove(groupSaving);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Group saving deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An error occurred while deleting the group saving", details = ex.Message });
        }
    }
}
