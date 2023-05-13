using StarSmithGames.Go.VibrationService;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace StarSmithGames.Go
{
	[RequireComponent(typeof(Button))]
    public class UIButtonComponent : MonoBehaviour
    {
        [SerializeField] private Button button;

		[Inject]
		private IVibrationService vibrationService;

		private void Awake()
		{
			if(button == null)
			{
				Debug.LogWarning("[UI] NRE Button Component!");
				button = GetComponent<Button>();
			}
			button.onClick.AddListener(OnClicked);
		}

		private void OnDestroy()
		{
			button.onClick.RemoveAllListeners();
		}

		private void OnClicked()
		{
			vibrationService.Vibrate();
		}
	}
}