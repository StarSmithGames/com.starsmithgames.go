using StarSmithGames.Core;

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

		public void Show<T>() where T : class, IView
		{
			GetAs<T>().Show();
		}
		public void Hide<T>() where T : class, IView
		{
			GetAs<T>().Hide();
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