using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

#if ADDRESSABLES
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
#endif

namespace StarSmithGames.Go.SceneManager
{
	public class SceneManager
	{
		public IProgressHandler ProgressHandler { get; private set; }

		private string currentScene;

		public SceneManager()
		{
			currentScene = GetActiveScene().name;
		}

		public Scene GetActiveScene()
		{
			return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
		}

		public void LoadSceneForce(int scene)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
		}

		private IEnumerator LoadFromBuild(string sceneName, bool allow)
		{
			var sceneNameUnity = $"{sceneName}";//.unity

			BuildProgressHandler handle = new();
			ProgressHandler = handle;

			handle.asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneNameUnity, LoadSceneMode.Single);
			handle.asyncOperation.allowSceneActivation = allow;
			yield return handle.asyncOperation;

			if (handle.asyncOperation.isDone)
			{
				currentScene = sceneName;
			}
			else
			{
				throw new SceneNoLoadedException();
			}
		}

#if ADDRESSABLES
		private Dictionary<string, IResourceLocation> resourceLocations = new Dictionary<string, IResourceLocation>();

		private IEnumerator LoadFromAddressables(string sceneName)
		{
			AddressablesProgressHandle progressHandle = new();
			ProgressHandler = progressHandle;

			var sceneNameUnity = $"{sceneName}";//.unity

			IResourceLocation location = null;
			yield return LoadLocation(sceneName, sceneNameUnity, (x) =>
			{
				location = x;
			});

			if (location != null)
			{
				Debug.Log($"[SceneManager] Start LoadSceneAsync {location}");
				progressHandle.sceneHandle = Addressables.LoadSceneAsync(location, LoadSceneMode.Single);
				yield return progressHandle.sceneHandle;
				Debug.Log($"[SceneManager] End LoadSceneAsync");

				Debug.Log($"[SceneManager] Start DownloadDependenciesAsync {sceneNameUnity}");
				progressHandle.dependenciesHandle = Addressables.DownloadDependenciesAsync(sceneNameUnity);
				yield return progressHandle.dependenciesHandle;
				Debug.Log($"[SceneManager] End DownloadDependenciesAsync");

				if (progressHandle.sceneHandle.Status == AsyncOperationStatus.Succeeded)
				{
					currentScene = sceneName;
				}
				else if (progressHandle.sceneHandle.Status == AsyncOperationStatus.Failed)
				{
					throw progressHandle.sceneHandle.OperationException;
				}
				else
				{
					throw new SceneNoLoadedException();
				}
			}
			else
			{
				throw new SceneNoLoadedException();
			}
		}

		private IEnumerator LoadLocation(string locationKey, string addressableLocationKey, Action<IResourceLocation> callback)
		{
			if (!resourceLocations.TryGetValue(locationKey, out var location))
			{
				var addressablesHandle = ProgressHandler as AddressablesProgressHandle;

				addressablesHandle.locationHandle = Addressables.LoadResourceLocationsAsync(addressableLocationKey);
				yield return addressablesHandle.locationHandle;

				if (addressablesHandle.locationHandle.IsDone &&
					addressablesHandle.locationHandle.Status == AsyncOperationStatus.Succeeded &&
					addressablesHandle.locationHandle.Result.Count > 0)
				{
					location = addressablesHandle.locationHandle.Result[0];
					if (!resourceLocations.ContainsKey(locationKey))
					{
						resourceLocations.Add(locationKey, location);
					}
					else
					{
						resourceLocations[locationKey] = location;
					}

				}
			}

			if(location == null)
			{
				throw new Exception("Location == null");
			}

			callback.Invoke(location);
		}
#endif
	}

	public class SceneNoLoadedException : Exception { }
}