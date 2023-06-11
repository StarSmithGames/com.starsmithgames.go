namespace StarSmithGames.Go
{
	public interface IEnableable
	{
		bool IsEnable { get; }

		void Enable(bool trigger);
	}
}