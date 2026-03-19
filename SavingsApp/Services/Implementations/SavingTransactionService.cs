using Microsoft.EntityFrameworkCore;
using SavingsApp.Data;
using SavingsApp.Models.Entities;
using SavingsApp.Models.Enums;
using SavingsApp.Services.Interfaces;

public class SavingTransactionService : ISavingTransactionService
{
    private readonly AppDbContext _context;
    private readonly INotificationService _notificationService;

    public SavingTransactionService(AppDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
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

        // Create notification
        var notification = new Notification
        {
            UserId = dto.UserId,
            Title = "💰 Amount Added",
            Message = $"${dto.Amount} has been added to '{goal.GoalName}'",
            Type = NotificationType.AmountAdded,
            RelatedEntityId = goal.Id,
            RelatedEntityType = "SavingGoal"
        };

        await _notificationService.CreateNotificationAsync(notification);

        // Check if goal is completed
        if (goal.Status == SavingStatus.Completed)
        {
            var completionNotification = new Notification
            {
                UserId = dto.UserId,
                Title = "🎉 Goal Reached!",
                Message = $"Congratulations! You've reached your '{goal.GoalName}' goal!",
                Type = NotificationType.GoalReached,
                RelatedEntityId = goal.Id,
                RelatedEntityType = "SavingGoal"
            };

            await _notificationService.CreateNotificationAsync(completionNotification);
        }
    }

    public async Task WithdrawAsync(WithdrawDto dto)
    {
        var goal = await _context.SavingGoals
            .FirstOrDefaultAsync(g => g.Id == dto.SavingGoalId && g.UserId == dto.UserId);

        if (goal == null)
            throw new Exception("Saving goal not found");

        if (dto.Amount <= 0)
            throw new Exception("Invalid amount");

        if (goal.CurrentAmount < dto.Amount)
            throw new Exception("Insufficient balance");

        // خصم المبلغ
        goal.CurrentAmount -= dto.Amount;

        // إضافة حركة سحب
        var transaction = new SavingTransaction
        {
            SavingGoalId = goal.Id,
            UserId = dto.UserId,
            Amount = dto.Amount,
            ContributionType = ContributionType.Withdraw,
            CreatedAt = DateTime.UtcNow
        };

        _context.SavingTransactions.Add(transaction);

        await _context.SaveChangesAsync();

        // Create notification
        var notification = new Notification
        {
            UserId = dto.UserId,
            Title = "💸 Amount Withdrawn",
            Message = $"${dto.Amount} has been withdrawn from '{goal.GoalName}'",
            Type = NotificationType.AmountWithdrawn,
            RelatedEntityId = goal.Id,
            RelatedEntityType = "SavingGoal"
        };

        await _notificationService.CreateNotificationAsync(notification);
    }
}
