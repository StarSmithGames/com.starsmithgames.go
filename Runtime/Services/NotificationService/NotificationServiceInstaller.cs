using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Notifications.Android;
using Unity.Notifications.iOS;

using UnityEngine;

using Zenject;

namespace StarSmithGames.Go.NotificationService
{
    [CreateAssetMenu(fileName = "NotificationServiceInstaller", menuName = "Installers/NotificationServiceInstaller")]
    public class NotificationServiceInstaller : ScriptableObjectInstaller
    {
        public NotificationSettings settings;
	}

	[System.Serializable]
	public class NotificationSettings
    {
		public List<NotificationChanel> chanels = new();
	}

	[System.Serializable]
	public class NotificationChanel
	{
		public string id;
		public string name;
		public string description;
		[Space]
		public List<Notification> notifications = new();
		[Space]
		public Importance importance = Importance.Default;
		public bool enableVibration = true;
		public bool enableLights = true;
		public bool canShowBadge = true;
		public bool canBypassDoNotDisturb = false;
		public LockScreenVisibility lockScreenVisibility = LockScreenVisibility.Public;
	}


	[System.Serializable]
    public class Notification
    {
        public string id;
        [Space]
		public string titleId;
        public string descriptionId;
        [Space]
        public string smallIcon;
        public string largeIcon;
		public NotificationStyle style = NotificationStyle.BigTextStyle;
		[Space]
		public int days = 0;
		public int hours = 0;
		public int minutes = 0;
	}

	//https://docs.unity3d.com/Packages/com.unity.mobile.notifications@1.4/api/Unity.Notifications.Android.AndroidNotificationChannel.html?q=canBypassDnd#:~:text=Properties-,CanBypassDnd,-Whether%20or%20not
}