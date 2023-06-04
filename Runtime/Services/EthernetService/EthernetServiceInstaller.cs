using UnityEngine;

using Zenject;

namespace StarSmithGames.Go.EthernetService
{
	[CreateAssetMenu(fileName = "EthernetServiceInstaller", menuName = "Installers/EthernetServiceInstaller")]
	public class EthernetServiceInstaller : ScriptableObjectInstaller
	{
		public EthernetMonitoringSettings settings;

		public override void InstallBindings()
		{
			Container.BindInstance(settings).WhenInjectedInto<EthernetService>();
			Container.BindInterfacesTo<EthernetService>().AsSingle().NonLazy();
		}
	}

	[System.Serializable]
	public class EthernetMonitoringSettings
	{
		public string pingAddress = "8.8.8.8";//Google Public DNS server
		public bool isMonitoringOnStart = true;
	}
}