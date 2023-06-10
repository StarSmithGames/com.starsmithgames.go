#if PURCHASING
using System.Collections.Generic;

using UnityEngine;

using Zenject;

namespace StarSmithGames.Go.InAppPurchaseService
{
    [CreateAssetMenu(fileName = "InAppPurchaseInstaller", menuName = "Installers/InAppPurchaseInstaller")]
    public class InAppPurchaseInstaller : ScriptableObjectInstaller
    {
		public List<InAppProduct> products = new();

		public override void InstallBindings()
		{
			Container.BindInstance(products).WhenInjectedInto<InAppPurchaseService>();
			Container.BindInterfacesTo<InAppPurchaseService>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
		}
	}
}
#endif