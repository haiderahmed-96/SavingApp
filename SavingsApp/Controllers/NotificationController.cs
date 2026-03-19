using Microsoft.AspNetCore.Mvc;
using SavingsApp.Models.Entities;
using SavingsApp.Services.Interfaces;
using AutoMapper;
using SavingsApp.Exceptions;
using SavingsApp.Models.Enums;

namespace SavingsApp.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new notification
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationDto dto)
        {
            var notification = _mapper.Map<Notification>(dto);
            var id = await _notificationService.CreateNotificationAsync(notification);

            return CreatedAtAction(nameof(GetById), new { id }, new
            {
                Id = id,
                Message = "Notification created successfully"
            });
        }

        /// <summary>
        /// Get all notifications for a user
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            if (userId <= 0)
                throw new BadRequestException("Valid user ID is required");

            var notifications = await _notificationService.GetUserNotificationsAsync(userId);
            var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);

            return Ok(notificationDtos);
        }

        /// <summary>
        /// Get unread notifications count
        /// </summary>
        [HttpGet("user/{userId}/unread-count")]
        public async Task<IActionResult> GetUnreadCount(int userId)
        {
            if (userId <= 0)
                throw new BadRequestException("Valid user ID is required");

            var count = await _notificationService.GetUnreadCountAsync(userId);

            return Ok(new { unreadCount = count });
        }

        /// <summary>
        /// Get notification by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Valid notification ID is required");

            var notification = await _notificationService.GetNotificationByIdAsync(id);

            if (notification == null)
                throw new NotFoundException("Notification not found");

            var notificationDto = _mapper.Map<NotificationDto>(notification);
            return Ok(notificationDto);
        }

        /// <summary>
        /// Mark notification as read
        /// </summary>
        [HttpPut("{id}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Valid notification ID is required");

            await _notificationService.MarkAsReadAsync(id);

            return Ok(new { message = "Notification marked as read" });
        }

        /// <summary>
        /// Mark all notifications as read for a user
        /// </summary>
        [HttpPut("user/{userId}/mark-all-as-read")]
        public async Task<IActionResult> MarkAllAsRead(int userId)
        {
            if (userId <= 0)
                throw new BadRequestException("Valid user ID is required");

            await _notificationService.MarkAllAsReadAsync(userId);

            return Ok(new { message = "All notifications marked as read" });
        }

        /// <summary>
        /// Update notification status
        /// </summary>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateNotificationStatusDto dto)
        {
            if (id <= 0)
                throw new BadRequestException("Valid notification ID is required");

            await _notificationService.UpdateNotificationStatusAsync(id, dto.Status);

            return Ok(new { message = "Notification status updated" });
        }

        /// <summary>
        /// Delete notification
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Valid notification ID is required");

            await _notificationService.DeleteNotificationAsync(id);

            return Ok(new { message = "Notification deleted successfully" });
        }

        /// <summary>
        /// Get notifications by type
        /// </summary>
        [HttpGet("user/{userId}/type/{type}")]
        public async Task<IActionResult> GetByType(int userId, NotificationType type)
        {
            if (userId <= 0)
                throw new BadRequestException("Valid user ID is required");

            var notifications = await _notificationService.GetNotificationsByTypeAsync(userId, type);
            var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);

            return Ok(notificationDtos);
        }
    }
}
