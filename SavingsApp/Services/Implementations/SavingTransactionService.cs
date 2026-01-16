using Microsoft.EntityFrameworkCore;
using SavingsApp.Data;
using SavingsApp.Models.Entities;
using SavingsApp.Models.Enums;

public class SavingTransactionService : ISavingTransactionService
{
    private readonly AppDbContext _context;

    public SavingTransactionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddTransactionAsync(AddSavingTransactionDto dto)
    {
        if (dto.Amount <= 0)
            throw new Exception("Amount must be greater than zero");

        var goal = await _context.SavingGoals
            .FirstOrDefaultAsync(g =>
                g.Id == dto.SavingGoalId &&
                g.UserId == dto.UserId);

        if (goal == null)
            throw new Exception("Saving goal not found");

        if (goal.Status != SavingStatus.Active)
            throw new Exception("Saving goal is not active");

        var transaction = new SavingTransaction
        {
            SavingGoalId = goal.Id,
            UserId = dto.UserId,
            Amount = dto.Amount,
            ContributionType = dto.ContributionType,
            CreatedAt = DateTime.UtcNow
        };

        goal.CurrentAmount += dto.Amount;

        if (goal.CurrentAmount >= goal.TargetAmount)
            goal.Status = SavingStatus.Completed;

        _context.SavingTransactions.Add(transaction);
        await _context.SaveChangesAsync();
    }
}
