using System.Collections.Generic;

using UnityEngine.Analytics;

namespace StarSmithGames.Go.AnalyticsSystem
{
	public interface IAnalyticsGroup
	{
		void LogEvent(string id);
		void LogEvent(string id, Dictionary<string, object> parameters);
	}

	public class UnityAnalyticsGroup : IAnalyticsGroup
	{
		public void LogEvent(string id)
		{
			Analytics.CustomEvent(id);
		}

		public void LogEvent(string id, Dictionary<string, object> parameters)
		{
			Analytics.CustomEvent(id, parameters);
		}
	}

	//stub
	public class AmplitudeAnalyticsGroup : IAnalyticsGroup
	{
		//private Amplitude instance;

		public AmplitudeAnalyticsGroup(string apiKey)
		{
			//instance = Amplitude.Instance;
			//instance.logging = true;
			//instance.trackSessionEvents(true);
			////instance.init(apiKey);
		}

		public void LogEvent(string id)
		{
			//instance.logEvent(id);
		}

		public void LogEvent(string id, Dictionary<string, object> parameters)
		{
			//instance.logEvent(id, parameters);
		}
	}

	//stub
	public class AppMetricaAnalyticsGroup : IAnalyticsGroup
	{
		public void LogEvent(string id)
		{
			// AppMetrica.Instance.ReportEvent(id);
			// AppMetrica.Instance.SendEventsBuffer();
		}

		public void LogEvent(string id, Dictionary<string, object> parameters)
		{
			// AppMetrica.Instance.ReportEvent(id, parameters);
			// AppMetrica.Instance.SendEventsBuffer();
		}
	}

	//stub
	public class FirebaseAnalyticsGroup : IAnalyticsGroup
	{
		public bool IsInitialized { get; private set; } = false;

		public FirebaseAnalyticsGroup()
		{
			//FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith((task) =>
			//{
			//	var dependencyStatus = task.Result;
			//	if (dependencyStatus == DependencyStatus.Available)
			//	{
			//		var app = FirebaseApp.DefaultInstance;

			//		FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
			//		//Crashlytics.IsCrashlyticsCollectionEnabled = true;

			//		IsInitialized = true;
			//		Debug.Log($"[FirebaseAnalyticsGroup] Initialized!");
			//	}
			//	else
			//	{
			//		Debug.LogError($"[FirebaseAnalyticsGroup] Could not resolve all Firebase dependencies: {dependencyStatus}");
			//	}
			//});
		}

		public void LogEvent(string id)
		{
			if (!IsInitialized) return;

			//FirebaseAnalytics.LogEvent(id);
		}

		public void LogEvent(string id, Dictionary<string, object> parameters)
		{
			if (!IsInitialized) return;

			//FirebaseAnalytics.LogEvent(id, IntoParameterArray(parameters));
		}

		//private Parameter[] IntoParameterArray(Dictionary<string, object> parameters)
		//{
		//	return parameters?.Where(x => x.Value != null).Select(x => new Parameter(x.Key, x.Value.ToString())).ToArray() ?? new Parameter[0];
		//}

		//private Dictionary<string, string> IntoStringDictionary(Dictionary<string, object> parameters)
		//{
		//	return parameters.ToDictionary(x => x.Key, x => x.Value.ToString());
		//}
	}

	//stub
	public class AppsFlyerAnalyticsGroup : IAnalyticsGroup
	{
		public void LogEvent(string id)
		{
			//AppsFlyer.sendEvent(id);
		}

		public void LogEvent(string id, Dictionary<string, object> parameters)
		{
			//AppsFlyer.sendEvent(id, parameters);
		}

		private Dictionary<string, string> ConvertParamDictionary(Dictionary<string, object> param)
		{
			var convertedParam = new Dictionary<string, string>();
			foreach (var keyValuePair in param)
			{
				convertedParam.Add(keyValuePair.Key, keyValuePair.Value.ToString());
			}

			return convertedParam;
		}
	}
}