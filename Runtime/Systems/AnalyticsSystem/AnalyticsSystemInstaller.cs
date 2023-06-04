using System.Collections.Generic;
using UnityEngine;

using Zenject;

namespace StarSmithGames.Go.AnalyticsSystem
{
	[CreateAssetMenu(fileName = "AnalyticsSystemInstaller", menuName = "Installers/AnalyticsSystemInstaller")]
	public class AnalyticsSystemInstaller : ScriptableObjectInstaller
	{
		public AnalysticsSettigns settigns;

		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<UnityAnalyticsGroup>().AsSingle().NonLazy();

			IAnalyticsGroup group = Container.Resolve<UnityAnalyticsGroup>();
			Container.BindInstance(new List<IAnalyticsGroup>() { group }).WhenInjectedInto<AnalyticsSystem>();
			Container.Bind<AnalyticsSystem>().AsSingle().NonLazy();
		}
	}

	[System.Serializable]
	public class AnalysticsSettigns
	{
		public string amplitudeApiKey;
	}
}