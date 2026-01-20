using SavingsApp.Models.Entities;

public interface ISavingGoalService
{
    Task<int> CreateSavingGoalAsync(SavingGoal goal);
    Task<SavingGoalDetailsDto> GetGoalDetailsAsync(int goalId, int userId);
    Task<List<SavingGoalListItemDto>> GetUserGoalsAsync(int userId);
    Task UpdateSavingGoalAsync(int goalId, UpdateSavingGoalDto dto);

}
