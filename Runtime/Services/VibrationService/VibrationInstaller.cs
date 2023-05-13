using UnityEngine;

using Zenject;

namespace StarSmithGames.Go.VibrationService
{
	[CreateAssetMenu(fileName = "VibrationInstaller", menuName = "Installers/VibrationInstaller")]
	public class VibrationInstaller : ScriptableObjectInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<VibrationService>().AsSingle().NonLazy();
		}
	}
}