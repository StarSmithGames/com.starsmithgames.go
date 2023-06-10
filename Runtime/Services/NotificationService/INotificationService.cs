#if NOTIFICATIONS
using UnityEngine;
using Unity.Notifications.Android;
using Unity.Notifications.iOS;
using System;
using System.Linq;

namespace StarSmithGames.Go.NotificationService
{
	public interface INotificationService
	{
		void AddNotification(string notificationId);
		void RemoveNotification(string notificationId);

		void RemoveAllNotifications();
	}

	public class NotificationService : INotificationService
	{
		private NotificationSettings settings;

		public NotificationService()
		{

			//if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
			//{
			//	Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
			//}

			//var permision = AndroidNotificationCenter.UserPermissionToPost;
			//bool isAllowed = AndroidNotificationCenter.UserPermissionToPost == PermissionStatus.Allowed ||;


#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidNotificationCenter.OnNotificationReceived += OnNotificationReceived;

			foreach (var chanel in settings.chanels)
			{
				var channel = new AndroidNotificationChannel()
				{
					Id = chanel.id,
					Name = chanel.name,
					Description = chanel.description,
					Importance = chanel.importance,
					CanBypassDnd = chanel.canBypassDoNotDisturb,
					CanShowBadge = chanel.canShowBadge,
					EnableLights = chanel.enableLights,
					EnableVibration = chanel.enableVibration,
					LockScreenVisibility = chanel.lockScreenVisibility,
				};

				AndroidNotificationCenter.RegisterNotificationChannel(channel);
			}
#elif UNITY_IOS && !UNITY_EDITOR
			iOSNotificationCenter.OnNotificationReceived += OnNotificationReceived;

			//foreach (var chanel in settings.chanels)
			//{

			//	iOSNotificationCenter.ScheduleNotification(new iOSNotification()
			//	{

			//	});
			//}
#endif
		}

		public void AddNotification(string notificationId)
		{
			var notification = settings.chanels.Select((x) => x.notifications.Find((y) => y.id == notificationId)).First();
			var chanel = settings.chanels.Find((x) => x.notifications.Contains(notification));

			var androidNotification = new AndroidNotification()
			{
				Title = "Title",//_localeService.GetString(notification.id),
				Text = "Text",//_localeService.GetString(notification.textid),
				SmallIcon = notification.smallIcon,
				LargeIcon = notification.largeIcon,
				Style = notification.style,

				FireTime = DateTime.Now + new TimeSpan(notification.days, notification.hours, notification.minutes),
			};

			var id = AndroidNotificationCenter.SendNotification(androidNotification, chanel.id);

#if UNITY_ANDROID && !UNITY_EDITOR
#elif UNITY_IOS && !UNITY_EDITOR
			iOSNotificationCenter.ScheduleNotification(new iOSNotification()
			{

			});
#endif
		}

		public void RemoveNotification(string notificationId)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidNotificationCenter.CancelNotification(0);
#elif UNITY_IOS && !UNITY_EDITOR
			iOSNotificationCenter.RemoveScheduledNotification(notificationId);
			iOSNotificationCenter.RemoveDeliveredNotification(notificationId);
#endif
		}

		public void RemoveAllNotifications()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidNotificationCenter.CancelAllNotifications();
#elif UNITY_IOS && !UNITY_EDITOR
			iOSNotificationCenter.RemoveAllScheduledNotifications();
			iOSNotificationCenter.RemoveAllDeliveredNotifications();
#endif
		}

#if UNITY_ANDROID && !UNITY_EDITOR
		private void OnNotificationReceived(AndroidNotificationIntentData data)
		{
			var msg = "Notification received : " + data.Id + "\n";
			msg += "\n Notification received: ";
			msg += "\n .Title: " + data.Notification.Title;
			msg += "\n .Body: " + data.Notification.Text;
			msg += "\n .Channel: " + data.Channel;
			Debug.Log(msg);
		}
#elif UNITY_IOS && !UNITY_EDITOR
		private void OnNotificationReceived(iOSNotification notification)
		{
			
		}
#endif
	}
}
#endif