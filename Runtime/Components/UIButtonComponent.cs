using StarSmithGames.Go.VibrationService;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace StarSmithGames.Go
{
	[RequireComponent(typeof(Button))]
	public abstract class UIButtonComponent : MonoBehaviour
	{
		[SerializeField]
		private Button button;

		[Inject]
		private IVibrationService vibrationService;

		protected virtual void Awake()
		{
			if(button == null)
			{
				Debug.LogWarning("[UI] NRE Button Component!");
				button = GetComponent<Button>();
			}
			button.onClick.AddListener(OnClicked);
		}

		protected virtual void OnDestroy()
		{
			button.onClick.RemoveAllListeners();
		}

		protected virtual void OnClicked()
		{
			vibrationService.Vibrate();
		}
	}
}