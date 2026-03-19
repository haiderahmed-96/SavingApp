using SavingsApp.Models.Entities;
using SavingsApp.Models.Enums;

namespace SavingsApp.Services.Interfaces
{
    public interface INotificationService
    {
        /// <summary>
        /// Create a new notification
        /// </summary>
        Task<int> CreateNotificationAsync(Notification notification);

        /// <summary>
        /// Get all notifications for a user
        /// </summary>
        Task<List<Notification>> GetUserNotificationsAsync(int userId);

        /// <summary>
        /// Get unread notifications count for a user
        /// </summary>
        Task<int> GetUnreadCountAsync(int userId);

        /// <summary>
        /// Get notification by id
        /// </summary>
        Task<Notification> GetNotificationByIdAsync(int notificationId);

        /// <summary>
        /// Mark notification as read
        /// </summary>
        Task MarkAsReadAsync(int notificationId);

        /// <summary>
        /// Mark all notifications as read for a user
        /// </summary>
        Task MarkAllAsReadAsync(int userId);

        /// <summary>
        /// Delete notification
        /// </summary>
        Task DeleteNotificationAsync(int notificationId);

        /// <summary>
        /// Update notification status
        /// </summary>
        Task UpdateNotificationStatusAsync(int notificationId, NotificationStatus status);

        /// <summary>
        /// Get notifications by type
        /// </summary>
        Task<List<Notification>> GetNotificationsByTypeAsync(int userId, NotificationType type);
    }
}
