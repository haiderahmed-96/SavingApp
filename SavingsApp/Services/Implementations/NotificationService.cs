using Microsoft.EntityFrameworkCore;
using SavingsApp.Data;
using SavingsApp.Models.Entities;
using SavingsApp.Models.Enums;
using SavingsApp.Services.Interfaces;

namespace SavingsApp.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;

        public NotificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateNotificationAsync(Notification notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            notification.CreatedAt = DateTime.Now;
            notification.Status = NotificationStatus.Unread;

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return notification.Id;
        }

        public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && n.Status == NotificationStatus.Unread)
                .CountAsync();
        }

        public async Task<Notification> GetNotificationByIdAsync(int notificationId)
        {
            return await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == notificationId);
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await GetNotificationByIdAsync(notificationId);

            if (notification == null)
                throw new InvalidOperationException("Notification not found");

            notification.Status = NotificationStatus.Read;
            notification.ReadAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId && n.Status == NotificationStatus.Unread)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.Status = NotificationStatus.Read;
                notification.ReadAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await GetNotificationByIdAsync(notificationId);

            if (notification == null)
                throw new InvalidOperationException("Notification not found");

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNotificationStatusAsync(int notificationId, NotificationStatus status)
        {
            var notification = await GetNotificationByIdAsync(notificationId);

            if (notification == null)
                throw new InvalidOperationException("Notification not found");

            notification.Status = status;

            if (status == NotificationStatus.Read && !notification.ReadAt.HasValue)
                notification.ReadAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetNotificationsByTypeAsync(int userId, NotificationType type)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && n.Type == type)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
    }
}
