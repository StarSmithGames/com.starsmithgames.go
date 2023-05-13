using UnityEngine;
using UnityEngine.Audio;

namespace StarSmithGames.Go.AudioService
{
	public interface IAudioService
	{
		void PlayMusic(AudioClip clip);
		void PlaySound(AudioClip clip);

		void SetMasterVolume(float value);
		void SetMusicVolume(float value);
		void SetSoundVolume(float value);
		
		void MuteMusic(bool trigger);
	}

	public class AudioService : IAudioService
	{
		private const string MIXER_MASTER = "MasterVolume";
		private const string MIXER_MUSIC = "MusicVolume";
		private const string MIXER_SFX = "SFXVolume";

		private AudioSource music;

		private AudioSource.Factory audioFactory;
		private AudioMixer audioMixer;

		public AudioService(AudioSource.Factory audioFactory, AudioMixer audioMixer)
		{
			this.audioFactory = audioFactory;
			this.audioMixer = audioMixer;
		}

		public void PlayMusic(AudioClip clip)
		{
			CreateMusic();
			music.PlayLoop(clip);
		}

		public void PlaySound(AudioClip clip)
		{
			var source = audioFactory.Create();
			if (audioMixer != null)
			{
				source.Source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
			}
			source.PlayOnce(clip);
		}

		public void SetMasterVolume(float value)
		{
			audioMixer.SetFloat(MIXER_MASTER, value);
		}

		public void SetMusicVolume(float value)
		{
			audioMixer.SetFloat(MIXER_MUSIC, value);
		}

		public void SetSoundVolume(float value)
		{
			audioMixer.SetFloat(MIXER_SFX, value);
		}

		public void MuteMusic(bool trigger)
		{
			CreateMusic();
			music.Mute(trigger);
		}

		private void CreateMusic()
		{
			if (music == null)
			{
				music = audioFactory.Create();

				if (audioMixer != null)
				{
					music.Source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
				}
			}
		}
	}
}