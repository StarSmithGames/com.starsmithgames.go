using System.Collections.Generic;

using UnityEngine;

namespace StarSmithGames.Go.AnalyticsSystem
{
    public class AnalyticsSystem
    {
		private List<IAnalyticsGroup> groups;

		public AnalyticsSystem(List<IAnalyticsGroup> groups)
		{
			this.groups = groups;
		}

		public void LogEvent(string id, Dictionary<string, object> parameters = null)
		{
			//id = saveLoad.GetStorage().IsPayUser.GetData() ? $"WHALE_{id}" : id;

			if (parameters == null)
			{
				for (int i = 0; i < groups.Count; i++)
				{
					groups[i].LogEvent(id);
				}
			}
			else
			{
				for (int i = 0; i < groups.Count; i++)
				{
					groups[i].LogEvent(id, parameters);
				}
			}
		}
	}
}