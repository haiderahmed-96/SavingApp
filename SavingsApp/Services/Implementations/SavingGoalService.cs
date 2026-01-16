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

        // 1️⃣ تحقق من اليوزر
        var users = await _context.Users.ToListAsync();
        Console.WriteLine($"Users count: {users.Count}");

        var userExists = await _context.Users
            .AnyAsync(u => u.Id == goal.UserId);

        if (!userExists)
            throw new Exception("User not found");

        // 2️⃣ تحقق من المبالغ
        if (goal.TargetAmount <= 0)
            throw new Exception("Target amount must be greater than zero");

        if (goal.DurationDays <= 0)
            throw new Exception("Duration days must be greater than zero");

        // 3️⃣ لا تخلي EF يدوخ
        goal.Id = 0;
        goal.CurrentAmount = 0;
        goal.Status = SavingStatus.Active;
        goal.CreatedAt = DateTime.UtcNow;

        // 4️⃣ افصل العلاقات
        goal.User = null;
        goal.SavingTransactions = null;
        goal.EventSaving = null;
        goal.TravelSaving = null;
        goal.GroupSaving = null;

        await _context.SavingGoals.AddAsync(goal);
        await _context.SaveChangesAsync();

        return goal.Id;
    }
}
