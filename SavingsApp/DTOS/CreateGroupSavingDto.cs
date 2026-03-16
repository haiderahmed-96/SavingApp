using System.ComponentModel.DataAnnotations;

public class CreateGroupSavingDto
{
    [Required(ErrorMessage = "SavingGoalId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "SavingGoalId must be greater than 0")]
    public int SavingGoalId { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0")]
    public int UserId { get; set; }
}
