using SavingsApp.Models.Entities;

public interface ISavingGoalService
{
    Task<int> CreateSavingGoalAsync(SavingGoal goal);
}
