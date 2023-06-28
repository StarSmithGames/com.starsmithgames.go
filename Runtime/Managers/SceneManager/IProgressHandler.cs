using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ADDRESSABLES
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
#endif

namespace StarSmithGames.Go.SceneManager
{
	public interface IProgressHandler
	{
		bool IsDone { get; }

		float GetProgress();
	}

	public class BuildProgressHandler : IProgressHandler
	{
		public bool IsDone => asyncOperation?.isDone ?? true;
		public bool IsAllowed => asyncOperation?.allowSceneActivation ?? true;

		public AsyncOperation asyncOperation = null;

		public float GetProgress()
		{
			return asyncOperation?.progress ?? 0f;
		}

		public void AllowSceneActivation()
		{
			asyncOperation.allowSceneActivation = true;
		}
	}

	public class FictProgressHandler : IProgressHandler
	{
		public bool IsDone
		{
			get
			{
				Tick();
				return progress == 1f;
			}
		}

		public float speed = 50f;
		private float progress = 0;

		private void Tick()
		{
			var delta = Time.deltaTime * speed * 0.01f;
			progress += delta;
			progress = Mathf.Clamp01(progress);
		}

		public float GetProgress()
		{
			return progress;
		}
	}

#if ADDRESSABLES
	public class AddressablesProgressHandle : IProgressHandler
	{
		public bool IsDone { get; private set; } = false;

		public AsyncOperationHandle<IList<IResourceLocation>> locationHandle;
		public AsyncOperationHandle sceneHandle;
		public AsyncOperationHandle dependenciesHandle;

		public float GetProgress()
		{
			var p1 = locationHandle.IsValid() ? locationHandle.PercentComplete : 0f;
			var p2 = sceneHandle.IsValid() ? sceneHandle.PercentComplete : 0f;
			var p3 = dependenciesHandle.IsValid() ? dependenciesHandle.PercentComplete : 0f;
			return (p1 + p2 + p3) / 3f;
		}
	}
#endif
}