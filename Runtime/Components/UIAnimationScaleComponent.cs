using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;

namespace StarSmithGames.Go
{
	public class UIAnimationScaleComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
		[SerializeField] private Transform target;
		[SerializeField] private float scaleMultiply = 0.95f;

		private Sequence sequence;
		private Vector3 startScale;
		private Vector3 endScale;

		private void Awake()
		{
			if (target == null)
			{
				target = transform;
			}

			startScale = target.localScale;
			endScale = startScale * scaleMultiply;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			sequence?.Complete(true);

			sequence = DOTween.Sequence();
			sequence
				.Append(target.DOScale(endScale, 0.1f))
				.SetEase(Ease.Linear);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			sequence?.Complete(true);

			sequence = DOTween.Sequence();
			sequence
				.Append(target.DOScale(startScale, 0.1f))
				.SetEase(Ease.Linear);
		}
	}
}
