using DG.Tweening;

using UnityEngine;

namespace StarSmithGames.Go
{
	public class AnimationScaleLoopComponent : MonoBehaviour
	{
		[SerializeField] private Vector3 scaleTo = new Vector3(1.2f, 1.2f, 1.2f);
		[Min(0.01f)]
		[SerializeField] private float duration = 0.2f;

		private void Start()
		{
			transform
				.DOScale(scaleTo, duration)
				.SetEase(Ease.InOutSine)
				.SetLoops(-1, LoopType.Yoyo);
		}
	}
}