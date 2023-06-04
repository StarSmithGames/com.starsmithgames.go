using UnityEditor;

using UnityEngine;

namespace StarSmithGames.Go.AnalyticsSystem
{
	[CustomEditor(typeof(AnalyticsSystemInstaller))]
	public class AnalyticsSystemInstallerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var obj = target as AnalyticsSystemInstaller;

			var lastUnity = obj.enableUnity;
			var lastAmplitude = obj.enableAmplitude;
			var lastAppsFlyer = obj.enableAppsFlyer;

			EditorGUI.BeginChangeCheck();
			base.OnInspectorGUI();
			if (EditorGUI.EndChangeCheck())
			{
				if(lastUnity != obj.enableUnity)
				{
					StarSmithDefineSymbols.EnableDefine(StarSmithDefineSymbols.ENABLE_UNITY_ANALYTICS, obj.enableUnity);
				}

				if (lastAmplitude != obj.enableAmplitude)
				{
					StarSmithDefineSymbols.EnableDefine(StarSmithDefineSymbols.ENABLE_AMPLITUDE, obj.enableAmplitude);
				}

				if (lastAppsFlyer != obj.enableAppsFlyer)
				{
					StarSmithDefineSymbols.EnableDefine(StarSmithDefineSymbols.ENABLE_APPSFLYER_ANALYTICS, obj.enableAmplitude);
				}
			}

			GUILayout.Space(50f);
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Refresh Defines", GUILayout.Width(100f)))
			{
				StarSmithDefineSymbols.Refresh();
			}
			GUILayout.EndHorizontal();
		}
	}
}