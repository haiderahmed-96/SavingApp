using SavingsApp.Models.Enums;

public class CreateNotificationDto
{
    public int UserId { get; set; }

    public string Title { get; set; }

    public string Message { get; set; }

    public NotificationType Type { get; set; }

    public int? RelatedEntityId { get; set; }

    public string RelatedEntityType { get; set; }
}
