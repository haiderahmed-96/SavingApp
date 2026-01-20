using Microsoft.EntityFrameworkCore;
using SavingsApp.Data;
using SavingsApp.Models.Entities;
using SavingsApp.Models.Enums;

public class SavingGoalService : ISavingGoalService
{
    private readonly AppDbContext _context;

    public SavingGoalService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateSavingGoalAsync(SavingGoal goal)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == goal.UserId);
        if (!userExists)
            throw new Exception("User not found");

        if (goal.TargetAmount <= 0)
            throw new Exception("Target amount must be greater than zero");

        if (goal.DurationDays <= 0)
            throw new Exception("Duration days must be greater than zero");

        goal.Id = 0;
        goal.CurrentAmount = 0;
        goal.Status = SavingStatus.Active;
        goal.CreatedAt = DateTime.UtcNow;

        goal.User = null;
        goal.SavingTransactions = null;
        goal.EventSaving = null;
        goal.TravelSaving = null;
        goal.GroupSaving = null;

        await _context.SavingGoals.AddAsync(goal);
        await _context.SaveChangesAsync();

        return goal.Id;
    }

    public async Task<SavingGoalDetailsDto> GetGoalDetailsAsync(int goalId, int userId)
    {
        var goal = await _context.SavingGoals
            .AsNoTracking()
            .Include(g => g.SavingTransactions)
            .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

        if (goal == null)
            throw new Exception("Saving goal not found");

        return new SavingGoalDetailsDto
        {
            Id = goal.Id,
            GoalName = goal.GoalName,
            TargetAmount = goal.TargetAmount,
            CurrentAmount = goal.CurrentAmount,
            DurationDays = goal.DurationDays,
            SavingType = goal.SavingType,
            Status = goal.Status,
            CreatedAt = goal.CreatedAt,
            Transactions = goal.SavingTransactions
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new SavingTransactionDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    ContributionType = (int)t.ContributionType,
                    CreatedAt = t.CreatedAt
                })
                .ToList()
        };
    }
   

public async Task<List<SavingGoalListItemDto>> GetUserGoalsAsync(int userId)
{
    var goals = await _context.SavingGoals
        .Where(g => g.UserId == userId)
        .OrderByDescending(g => g.CreatedAt)
        .ToListAsync();

    return goals.Select(g => new SavingGoalListItemDto
    {
        Id = g.Id,
        GoalName = g.GoalName,
        TargetAmount = g.TargetAmount,
        CurrentAmount = g.CurrentAmount,
        DurationDays = g.DurationDays,
        SavingType = g.SavingType,
        Status = g.Status,
        CreatedAt = g.CreatedAt,
        ProgressPercent = g.TargetAmount == 0 ? 0 : Math.Round((g.CurrentAmount / g.TargetAmount) * 100, 2)
    }).ToList();
}
    public async Task UpdateSavingGoalAsync(int goalId, UpdateSavingGoalDto dto)
    {
        var goal = await _context.SavingGoals
            .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == dto.UserId);

        if (goal == null)
            throw new Exception("Saving goal not found");

        if (string.IsNullOrWhiteSpace(dto.GoalName))
            throw new Exception("Goal name is required");

        if (dto.TargetAmount <= 0)
            throw new Exception("Target amount must be greater than zero");

        if (dto.DurationDays <= 0)
            throw new Exception("Duration days must be greater than zero");

        // CurrentAmount
        goal.GoalName = dto.GoalName;
        goal.TargetAmount = dto.TargetAmount;
        goal.DurationDays = dto.DurationDays;

        // (اختياري) إذا كان الهدف مكتمل قبل ورفعنا TargetAmount فوق CurrentAmount
        if (goal.Status == SavingStatus.Completed && goal.CurrentAmount < goal.TargetAmount)
            goal.Status = SavingStatus.Active;

        await _context.SaveChangesAsync();
    }


}
