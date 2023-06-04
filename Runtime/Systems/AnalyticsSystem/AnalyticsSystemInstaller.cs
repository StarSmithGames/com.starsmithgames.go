using System.Collections.Generic;

using UnityEngine;

using Zenject;

namespace StarSmithGames.Go.AnalyticsSystem
{
	[CreateAssetMenu(fileName = "AnalyticsSystemInstaller", menuName = "Installers/AnalyticsSystemInstaller")]
	public class AnalyticsSystemInstaller : ScriptableObjectInstaller
	{
		public bool enableUnity = false;
		public bool enableAmplitude = false;
		public bool enableAppsFlyer = false;

#if ENABLE_AMPLITUDE_ANALYTICS
		public AmplitudeSettings amplitudeSettings;
#endif
#if ENABLE_APPSFLYER_ANALYTICS
		public AppsFlyerSettings appsFlyerSettings;
#endif

		public override void InstallBindings()
		{
			//Bind Groups
#if ENABLE_UNITY_ANALYTICS
			Container.BindInterfacesAndSelfTo<UnityAnalyticsGroup>().AsSingle().NonLazy();
#endif
#if ENABLE_AMPLITUDE_ANALYTICS
			Container.BindInstance(amplitudeSettings).WhenInjectedInto<AmplitudeAnalyticsGroup>();
			Container.BindInterfacesAndSelfTo<AmplitudeAnalyticsGroup>().AsSingle().NonLazy();
#endif
#if ENABLE_APPSFLYER_ANALYTICS
			Container.BindInstance(appsFlyerSettings).WhenInjectedInto<AppsFlyerAnalyticsGroup>();
			Container.BindInterfacesAndSelfTo<AppsFlyerAnalyticsGroup>().AsSingle().NonLazy();
#endif

			//List of Groups
			Container.BindInstance(new List<IAnalyticsGroup>() {
#if ENABLE_UNITY_ANALYTICS
				Container.Resolve<UnityAnalyticsGroup>(),
#endif
#if ENABLE_AMPLITUDE_ANALYTICS
				Container.Resolve<AmplitudeAnalyticsGroup>(),
#endif
#if ENABLE_APPSFLYER_ANALYTICS
				Container.Resolve<AppsFlyerAnalyticsGroup>(),
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

#if ENABLE_APPSFLYER_ANALYTICS
	[System.Serializable]
	public class AppsFlyerSettings
	{
		public string devKey;
		public string appID;
	}
#endif
}