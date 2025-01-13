using Microsoft.AspNetCore.Mvc;
using NotificationAPI.Models;
using NotificationAPI.Services;

namespace NotificationAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class NotificationController : ControllerBase
	{
		private readonly ILogger<NotificationController> _logger; // Inject Logger
		private static List<PushSubscription> _subscriptions = new List<PushSubscription>();
		private readonly NotificationService _notificationService;

		public NotificationController(ILogger<NotificationController> logger)
		{
			_logger = logger; // Gán Logger
			_notificationService = new NotificationService();
		}

		[HttpPost("subscribe")]
		public IActionResult Subscribe([FromBody] PushSubscription subscription)
		{
			if (!_subscriptions.Any(s => s.Endpoint == subscription.Endpoint))
			{
				_subscriptions.Add(subscription);
				_logger.LogInformation("New subscription added: {Subscription}", System.Text.Json.JsonSerializer.Serialize(subscription)); // Ghi log thông tin subscription
			}
			else
			{
				_logger.LogInformation("Subscription already exists: {Subscription}", System.Text.Json.JsonSerializer.Serialize(subscription));
			}

			return Ok(new { message = "Subscription added successfully." });
		}

		[HttpPost("send")]
		public IActionResult SendNotification([FromBody] NotificationRequest request)
		{
			foreach (var subscription in _subscriptions)
			{
				_notificationService.SendNotification(subscription, request.Title, request.Body);
				_logger.LogInformation("Notification sent to subscription: {Subscription}", System.Text.Json.JsonSerializer.Serialize(subscription));
			}

			return Ok(new { message = "Notifications sent successfully." });
		}
	}

	public class NotificationRequest
	{
		public string Title { get; set; }
		public string Body { get; set; }
	}
}
