using StarSmithGames.Core;
using StarSmithGames.Go.AnalyticsSystem;

using System.Collections.Generic;
using System.Linq;

using UnityEditor;

namespace StarSmithGames
{
	[InitializeOnLoad]
	public class StarSmithDefineSymbols
	{
		public const string ENABLE_UNITY_ANALYTICS = "ENABLE_UNITY_ANALYTICS";
		public const string ENABLE_AMPLITUDE = "ENABLE_AMPLITUDE_ANALYTICS";

		public static readonly string[] DEFINES = new string[]
		{
			ENABLE_UNITY_ANALYTICS,
			ENABLE_AMPLITUDE,
		};

		static StarSmithDefineSymbols()
		{
			Refresh();
		}

		public static void Refresh()
		{
			var asset = AssetDatabaseExtensions.LoadAsset<AnalyticsSystemInstaller>();

			if (asset != null)
			{
				var unityDefines = GetUnityDefines();
				unityDefines.AddRange(DEFINES.Except(unityDefines));

				if (!asset.enableAmplitude)
				{
					unityDefines.Remove(ENABLE_AMPLITUDE);
				}

				SaveDefines(unityDefines);
			}
			else
			{
				for (int i = 0; i < DEFINES.Length; i++)
				{
					TryRemoveDefine(DEFINES[i]);
				}
			}
		}

		public static void EnableDefine(string define, bool trigger)
		{
			var unityDefines = GetUnityDefines();
			if (trigger)
			{
				if (!unityDefines.Contains(define))
				{
					unityDefines.Add(define);
				}
			}
			else
			{
				if (unityDefines.Contains(define))
				{
					unityDefines.Remove(define);
				}
			}

			SaveDefines(unityDefines);
		}

		public static List<string> GetUnityDefines()
		{
			string scriptingDefinesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
			return scriptingDefinesString.Split(';').ToList();
		}

		private static void TryRemoveDefine(string define)
		{
			var unityDefines = GetUnityDefines();
			if (unityDefines.Contains(define))
			{
				unityDefines.Remove(define);
			}
			SaveDefines(unityDefines);
		}

		private static void SaveDefines(List<string> unityDefines)
		{
			PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", unityDefines.ToArray()));
		}
	}
}