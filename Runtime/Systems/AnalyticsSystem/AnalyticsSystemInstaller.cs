using System.Collections.Generic;

using UnityEngine;

using Zenject;

namespace StarSmithGames.Go.AnalyticsSystem
{
	[CreateAssetMenu(fileName = "AnalyticsSystemInstaller", menuName = "Installers/AnalyticsSystemInstaller")]
	public class AnalyticsSystemInstaller : ScriptableObjectInstaller
	{
		public bool enableUnity = true;
		public bool enableAmplitude = true;

#if ENABLE_AMPLITUDE_ANALYTICS
		public AmplitudeSettings amplitudeSettings;
#endif

		public override void InstallBindings()
		{
#if ENABLE_UNITY_ANALYTICS
			Container.BindInterfacesAndSelfTo<UnityAnalyticsGroup>().AsSingle().NonLazy();
#endif
#if ENABLE_AMPLITUDE_ANALYTICS
			Container.BindInstance(amplitudeSettings).WhenInjectedInto<AmplitudeAnalyticsGroup>();
			Container.BindInterfacesAndSelfTo<AmplitudeAnalyticsGroup>().AsSingle().NonLazy();
#endif

			Container.BindInstance(new List<IAnalyticsGroup>() {
#if ENABLE_UNITY_ANALYTICS
				Container.Resolve<UnityAnalyticsGroup>(),
#endif
#if ENABLE_AMPLITUDE_ANALYTICS
				Container.Resolve<AmplitudeAnalyticsGroup>()
#endif
			}).WhenInjectedInto<AnalyticsSystem>();
			Container.Bind<AnalyticsSystem>().AsSingle().NonLazy();
		}
	}

#if ENABLE_AMPLITUDE_ANALYTICS
	[System.Serializable]
	public class AmplitudeSettings
	{
		public string apiKey;
	}
#endif
}