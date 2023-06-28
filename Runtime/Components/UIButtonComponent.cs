using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;

namespace StarSmithGames.Go
{
	public class UIButtonComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
	{
		[SerializeField] private Transform target;
		[SerializeField] private float scaleMultiply = 0.95f;

		private Sequence sequence;
		private Vector3 startScale;
		private Vector3 endScale;

		private void Awake()
		{
			startScale = target.localScale;
			endScale = startScale * scaleMultiply;
		}

		public virtual void OnPointerDown(PointerEventData eventData)
		{
			sequence?.Kill(true);
			sequence = DOTween.Sequence();
			sequence
				.Append(target.DOScale(endScale, 0.1f))
				.SetEase(Ease.Linear);
		}

		public virtual void OnPointerUp(PointerEventData eventData)
		{
			sequence?.Kill(true);
			sequence = DOTween.Sequence();
			sequence
				.Append(target.DOScale(startScale, 0.1f))
				.SetEase(Ease.Linear);
		}

		public virtual void OnPointerClick(PointerEventData eventData) { }
	}
}