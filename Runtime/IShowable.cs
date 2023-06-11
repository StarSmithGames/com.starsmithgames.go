using StarSmithGames.Core;

using System;

namespace StarSmithGames.Go
{
	public interface IShowable : IEnableable
	{
		bool IsShowing { get; }
		bool IsInProcess { get; }

		void Show(Action callback = null);
		void Hide(Action callback = null);
	}
}