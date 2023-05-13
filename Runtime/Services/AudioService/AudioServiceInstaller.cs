using UnityEngine;
using UnityEngine.Audio;

using Zenject;

namespace StarSmithGames.Go.AudioService
{
	[CreateAssetMenu(fileName = "AudioServiceInstaller", menuName = "Installers/AudioServiceInstaller")]
    public class AudioServiceInstaller : ScriptableObjectInstaller
    {
		public int poolSize = 2;
		public AudioMixer audioMixer;

		public override void InstallBindings()
		{
			Container.BindFactory<AudioSource, AudioSource.Factory>()
				.FromMonoPoolableMemoryPool((x) => x.WithInitialSize(poolSize)
				.FromNewComponentOnNewGameObject())
				.WhenInjectedInto<AudioService>();
			Container.BindInstance(audioMixer).WhenInjectedInto<AudioService>();
			Container.BindInterfacesTo<AudioService>().AsSingle().NonLazy();
		}
	}
}