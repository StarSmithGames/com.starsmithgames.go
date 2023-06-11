using DG.Tweening;

using System;

using UnityEngine;

using StarSmithGames.Core;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StarSmithGames.Go
{
	public interface IView : IShowable { }

	public abstract class ViewBase : MonoBehaviour, IView
	{
		public bool IsEnable { get; protected set; }
		public bool IsShowing { get; protected set; }
		public bool IsInProcess { get; protected set; }

		public CanvasGroup canvasGroup;

		public virtual void Show(Action callback = null)
		{
			IsInProcess = true;
			canvasGroup.alpha = 0f;
			canvasGroup.Enable(true, false);
			IsShowing = true;

			Sequence sequence = DOTween.Sequence();

			sequence
				.Append(canvasGroup.DOFade(1f, 0.2f))
				.AppendCallback(() =>
				{
					callback?.Invoke();
					IsInProcess = false;
				});
		}

		public virtual void Hide(Action callback = null)
		{
			IsInProcess = true;

			Sequence sequence = DOTween.Sequence();

			sequence
				.Append(canvasGroup.DOFade(0f, 0.15f))
				.AppendCallback(() =>
				{
					canvasGroup.Enable(false);
					IsShowing = false;
					callback?.Invoke();

					IsInProcess = false;
				});
		}

		public virtual void Enable(bool trigger)
		{
			canvasGroup.Enable(trigger);
			IsShowing = trigger;
			IsEnable = trigger;
		}

		[ContextMenu("Open Close")]
		private void OpenClose()
		{
			Enable(canvasGroup.alpha == 0f ? true : false);
#if UNITY_EDITOR
			EditorUtility.SetDirty(gameObject);
#endif
		}
	}

	public abstract class ViewPopupBase : ViewBase
	{
		public Transform window;

		public override void Show(Action callback = null)
		{
			window.localScale = Vector3.zero;

			IsInProcess = true;
			canvasGroup.alpha = 0f;
			canvasGroup.Enable(true, false);
			IsShowing = true;

			Sequence sequence = DOTween.Sequence();

			sequence
				.Append(canvasGroup.DOFade(1f, 0.2f))
				.Join(window.DOScale(1, 0.35f).SetEase(Ease.OutBounce))
				.AppendCallback(() =>
				{
					callback?.Invoke();
					IsInProcess = false;
				});
		}

		public override void Hide(Action callback = null)
		{
			window.localScale = Vector3.one;

			IsInProcess = true;

			Sequence sequence = DOTween.Sequence();

			sequence
				.Append(window.DOScale(0, 0.25f).SetEase(Ease.InBounce))
				.Join(canvasGroup.DOFade(0f, 0.25f))
				.AppendCallback(() =>
				{
					canvasGroup.Enable(false);
					IsShowing = false;
					callback?.Invoke();

					IsInProcess = false;
				});
		}
	}

	public abstract class ViewQuartBase : ViewPopupBase
	{
		public override void Show(Action callback = null)
		{
			window.localScale = Vector3.zero;

			IsInProcess = true;
			canvasGroup.alpha = 0f;
			canvasGroup.Enable(true, false);
			IsShowing = true;

			Sequence sequence = DOTween.Sequence();

			sequence
				.Append(canvasGroup.DOFade(1f, 0.2f))
				.Join(window.DOScale(1, 0.35f).SetEase(Ease.OutQuart))
				.AppendCallback(() =>
				{
					callback?.Invoke();
					IsInProcess = false;
				});
		}

		public override void Hide(Action callback = null)
		{
			window.localScale = Vector3.one;

			IsInProcess = true;

			Sequence sequence = DOTween.Sequence();

			sequence
				.Append(canvasGroup.DOFade(0f, 0.15f))
				.Join(window.DOScale(0, 0.25f).SetEase(Ease.InBounce))
				.AppendCallback(() =>
				{
					canvasGroup.Enable(false);
					IsShowing = false;
					callback?.Invoke();

					IsInProcess = false;
				});
		}
	}
}