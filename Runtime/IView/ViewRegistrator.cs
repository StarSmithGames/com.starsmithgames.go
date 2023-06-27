using StarSmithGames.Core;

using System;
using System.Linq;

namespace StarSmithGames.Go
{
	public class ViewRegistrator : Registrator<IView>
	{
		public bool IsAnyShowing()
		{
			return registers.Any((x) => x.IsShowing);
		}
		public bool IsAllHided()
		{
			return registers.All((x) => !x.IsShowing);
		}

		public void Show<T>(Action callback = null) where T : class, IView
		{
			GetAs<T>().Show(callback);
		}
		public void Hide<T>(Action callback = null) where T : class, IView
		{
			GetAs<T>().Hide(callback);
		}

		public void HideAll()
		{
			for (int i = 0; i < registers.Count; i++)
			{
				registers[i].Hide();
			}
		}
	}
}