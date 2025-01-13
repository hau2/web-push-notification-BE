using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using System.Text.Json;

namespace NotificationAPI.Services
{
	public class NotificationService
	{
		private readonly string _vapidPublicKey = "BO5-bgJqcpF87kkwGV34NLuezchy4bQOIf3-T2eT5Hh9V4rSNyWczVB6SrD84ZwoEwi971m1ZtecIAjlmrZOg1U";
		private readonly string _vapidPrivateKey = "JzjVF9BGbqlJ5tu_sHmEauyU7mi8xUNMWuR4SHobVrg";

		public void SendNotification(PushSubscription subscription, string title, string body)
		{
			// Khởi tạo PushServiceClient
			var pushService = new PushServiceClient();

			// Sử dụng constructor của VapidAuthentication để truyền publicKey và privateKey
			pushService.DefaultAuthentication = new VapidAuthentication(_vapidPublicKey, _vapidPrivateKey)
			{
				Subject = "mailto:your-email@example.com"
			};

			// Dữ liệu thông báo
			var notification = new PushMessage(JsonSerializer.Serialize(new
			{
				title,
				body
			}));

			// Gửi thông báo
			pushService.RequestPushMessageDeliveryAsync(subscription, notification).Wait();
		}
	}
}
