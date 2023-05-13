using UnityEngine;

using Zenject;

namespace StarSmithGames.Go.ApplicationHandler
{
    public class ApplicationHandler : MonoBehaviour
    {
		private SignalBus signalBus;

		[Inject]
		private void Construct(SignalBus signalBus)
		{
			this.signalBus = signalBus;

			Application.runInBackground = true;
		}

		private void OnApplicationFocus(bool focus)
		{
			signalBus?.Fire(new SignalOnApplicationFocusChanged() { trigger = focus });
		}

		private void OnApplicationPause(bool pause)
		{
			signalBus?.Fire(new SignalOnApplicationPauseChanged() { trigger = pause });
		}
	}
}