using System;
using System.Collections;
using UnityEngine;

using Zenject;

namespace StarSmithGames.Go.EthernetService
{
	public interface IEthernetService
	{
		event Action<bool> OnConnectionChanged;

		public bool IsConnected { get; }

		public bool IsMonitoring { get; }

		public void StartMonitoring();
		public void StopMonitoring();
	}

	public class EthernetService : IEthernetService, IInitializable
	{
		public event Action<bool> OnConnectionChanged;

		public bool IsConnected
		{
			get => isConnected;
			private set
			{
				if (isConnected == value)
				{
					return;
				}

				Debug.Log($"[InternetStatusService] Connection changed: {value}");

				isConnected = value;
				OnConnectionChanged?.Invoke(value);
			}
		}
		private bool isConnected = false;

		public bool IsMonitoring => monitoringCoroutine != null;
		private Coroutine monitoringCoroutine;

		[Inject]
		private AsyncManager.AsyncManager asyncManager;

		private Ping ping;

		private EthernetMonitoringSettings settings;

		public EthernetService(EthernetMonitoringSettings settings)
		{
			this.settings = settings;
		}

		public void Initialize()
		{
			if (settings.isMonitoringOnStart)
			{
				StartMonitoring();
			}
		}

		public void StartMonitoring()
		{
			monitoringCoroutine = asyncManager.StartCoroutine(CheckInternetConnection());
		}

		public void StopMonitoring()
		{
			if(monitoringCoroutine != null)
			{
				asyncManager.StopCoroutine(monitoringCoroutine);
				monitoringCoroutine = null;

				ping.DestroyPing();
			}
		}

		private IEnumerator CheckInternetConnection()
		{
			ping?.DestroyPing();
			ping = new Ping(settings.pingAddress);

			yield return null;

			while (true)
			{
				IsConnected = Application.internetReachability != NetworkReachability.NotReachable ? ping.isDone : false;
				yield return null;
			}
		}
	}
}