using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using System.Text.Json;

namespace NotificationAPI.Services
{
	public class NotificationService
	{
		private readonly string _vapidPublicKey = "BOmI-_Eat9iH0TYjh7D3E1XOwmmlfCW6wpK8J5pV-MezE_VwSkafAcn1OhAjLqK1l1UqfaCZ7ORkYWROlWLDqEU";
		private readonly string _vapidPrivateKey = "ndmKxdZkvxOMJmTLyD4WMRDAy1h82DRsBqSNMr5Euo0";

		public void SendNotification(PushSubscription subscription, string title, string body, string url="/")
		{
			var pushService = new PushServiceClient();

			pushService.DefaultAuthentication = new VapidAuthentication(_vapidPublicKey, _vapidPrivateKey)
			{
				Subject = "mailto:your-email@example.com"
			};

			var notification = new PushMessage(JsonSerializer.Serialize(new
			{
				title,
				body,
				url
			}));

			pushService.RequestPushMessageDeliveryAsync(subscription, notification).Wait();
		}
	}
}
