using Microsoft.AspNetCore.Mvc;
using SavingsApp.Data;
using SavingsApp.Models.Entities;
using SavingsApp.Models.Enums;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SavingsApp.Exceptions;

[ApiController]
[Route("api/group-saving")]
public class GroupSavingController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GroupSavingController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Create a new group saving
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGroupSavingDto dto)
    {
        var goal = await _context.SavingGoals
            .FirstOrDefaultAsync(g => g.Id == dto.SavingGoalId);

        if (goal == null)
            throw new NotFoundException("Saving goal not found");

        var existingGroup = await _context.GroupSavings
            .FirstOrDefaultAsync(g => g.SavingGoalId == dto.SavingGoalId);

        if (existingGroup != null)
            throw new ConflictException("Group saving already exists for this goal");

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

        _context.GroupSavings.Add(groupSaving);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { goalId = dto.SavingGoalId }, 
            new { id = groupSaving.Id, message = "Group saving created successfully" });
    }

    /// <summary>
    /// Get group saving details
    /// </summary>
    [HttpGet("{goalId}")]
    public async Task<IActionResult> Get(int goalId)
    {
        if (goalId <= 0)
            throw new BadRequestException("Invalid goal ID");

        var groupSaving = await _context.GroupSavings
            .Include(g => g.GroupMembers)
            .FirstOrDefaultAsync(g => g.SavingGoalId == goalId);

        if (groupSaving == null)
            throw new NotFoundException("Group saving not found for this goal");

        return Ok(groupSaving);
    }

    /// <summary>
    /// Add a member to group saving
    /// </summary>
    [HttpPost("{goalId}/members")]
    public async Task<IActionResult> AddMember(int goalId, [FromBody] AddGroupMemberDto dto)
    {
        if (goalId <= 0)
            throw new BadRequestException("Invalid goal ID");

        var groupSaving = await _context.GroupSavings
            .FirstOrDefaultAsync(g => g.SavingGoalId == goalId);

        if (groupSaving == null)
            throw new NotFoundException("Group saving not found");

        var existingMember = await _context.GroupMembers
            .FirstOrDefaultAsync(m => m.GroupSavingId == groupSaving.Id && m.UserId == dto.UserId);

        if (existingMember != null)
            throw new ConflictException("User is already a member of this group");

        var member = _mapper.Map<GroupMember>(dto);
        member.GroupSavingId = groupSaving.Id;
        member.Role = GroupRole.Member;

        _context.GroupMembers.Add(member);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Member added successfully" });
    }

    /// <summary>
    /// Remove a member from group saving
    /// </summary>
    [HttpDelete("{goalId}/members/{userId}")]
    public async Task<IActionResult> RemoveMember(int goalId, int userId)
    {
        if (goalId <= 0 || userId <= 0)
            throw new BadRequestException("Invalid goal ID or user ID");

        var groupSaving = await _context.GroupSavings
            .FirstOrDefaultAsync(g => g.SavingGoalId == goalId);

        if (groupSaving == null)
            throw new NotFoundException("Group saving not found");

        var member = await _context.GroupMembers
            .FirstOrDefaultAsync(m => m.GroupSavingId == groupSaving.Id && m.UserId == userId);

        if (member == null)
            throw new NotFoundException("Member not found");

        if (member.Role == GroupRole.Owner)
            throw new BadRequestException("Cannot remove the owner of the group");

        _context.GroupMembers.Remove(member);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Member removed successfully" });
    }

    /// <summary>
    /// Delete group saving
    /// </summary>
    [HttpDelete("{goalId}")]
    public async Task<IActionResult> Delete(int goalId)
    {
        if (goalId <= 0)
            throw new BadRequestException("Invalid goal ID");

        var groupSaving = await _context.GroupSavings
            .FirstOrDefaultAsync(g => g.SavingGoalId == goalId);

        if (groupSaving == null)
            throw new NotFoundException("Group saving not found");

        _context.GroupSavings.Remove(groupSaving);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Group saving deleted successfully" });
    }
}
