using System;

namespace StarSmithGames.Go
{
	public interface IShowable
	{
		bool IsShowing { get; }
		bool IsInProcess { get; }

		void Enable(bool trigger);
		void Show(Action callback = null);
		void Hide(Action callback = null);
	}
}