using SavingsApp.Models.Enums;

public class NotificationDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; }

    public string Message { get; set; }

    public NotificationType Type { get; set; }

    public NotificationStatus Status { get; set; }

    public int? RelatedEntityId { get; set; }

    public string RelatedEntityType { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ReadAt { get; set; }
}
