﻿using UnityEditor;

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
			}
		}
	}
}