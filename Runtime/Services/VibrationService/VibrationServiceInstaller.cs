using UnityEngine;

using Zenject;

namespace StarSmithGames.Go.VibrationService
{
	[CreateAssetMenu(fileName = "VibrationServiceInstaller", menuName = "Installers/VibrationServiceInstaller")]
	public class VibrationServiceInstaller : ScriptableObjectInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<VibrationService>().AsSingle().NonLazy();
		}
	}
}