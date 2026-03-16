using System.ComponentModel.DataAnnotations;

public class CreateTravelSavingDto
{
    [Required(ErrorMessage = "SavingGoalId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "SavingGoalId must be greater than 0")]
    public int SavingGoalId { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Country must be between 2 and 100 characters")]
    public string Country { get; set; }

    [Required(ErrorMessage = "CurrencyType is required")]
    [Range(1, int.MaxValue, ErrorMessage = "CurrencyType must be a valid enum value")]
    public int CurrencyType { get; set; }

    [Required(ErrorMessage = "EquivalentAmount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "EquivalentAmount must be greater than 0")]
    public decimal EquivalentAmount { get; set; }
}