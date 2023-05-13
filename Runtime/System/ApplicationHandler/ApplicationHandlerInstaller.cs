using Zenject;

namespace StarSmithGames.Go.ApplicationHandler
{
	public class ApplicationHandlerInstaller : Installer<ApplicationHandlerInstaller>
	{
		public override void InstallBindings()
		{
			Container.DeclareSignal<SignalOnApplicationPauseChanged>();
			Container.DeclareSignal<SignalOnApplicationFocusChanged>();

			Container.Bind<ApplicationHandler>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
		}
	}
}