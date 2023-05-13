using UnityEngine;

using Zenject;

namespace StarSmithGames.Go.AudioService
{
	[RequireComponent(typeof(UnityEngine.AudioSource))]
	public class AudioSource : PoolableObject
	{
		public UnityEngine.AudioSource Source
		{
			get
			{
				if (source == null)
				{
					source = GetComponent<UnityEngine.AudioSource>();
				}

				return source;
			}
		}
		private UnityEngine.AudioSource source;

		public bool IsPlaying { get; private set; } = false;
		public bool isLoop { get; private set; } = false;

		private float td = 0;
		private float playTime = 0;

		private void Update()
		{
			if (isLoop) return;

			if (IsPlaying)
			{
				td += Time.deltaTime;

				if (td >= playTime)
				{
					td = 0;

					IsPlaying = false;
					OnPlayingChanged();
				}
			}
		}

		public void PlayLoop(AudioClip clip)
		{
			Source.clip = clip;
			Source.Play();

			Loop(true);
		}

		public void PlayOnce(AudioClip clip)
		{
			Loop(false);

			td = 0;
			playTime = clip.length;
			Source.PlayOneShot(clip);

			IsPlaying = true;
			OnPlayingChanged();
		}

		public void Mute(bool trigger)
		{
			Source.mute = trigger;
		}

		public void Loop(bool trigger)
		{
			isLoop = trigger;
			Source.loop = trigger;
		}

		private void OnPlayingChanged()
		{
			if (!IsPlaying)
			{
				DespawnIt();
			}
		}

		public class Factory : PlaceholderFactory<AudioSource> { }
	}
}