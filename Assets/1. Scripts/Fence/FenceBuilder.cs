using System.Collections.Generic;
using Events.GameplayEvents;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.EventSystem;

namespace Fence
{
	public class FenceBuilder : BetterMonoBehaviour
	{
		[SerializeField] private GameObject fencePrefab = null;
		[SerializeField] private float timeUntilFenceDisappear = 5.0f;
		
		private readonly List<GameObject> fences = new List<GameObject>();

		private void Awake()
		{
			EventManager.Instance.AddListener<FenceCompletedEvent>(OnFenceCompleted);
		}

		private void OnDestroy()
		{
			if (EventManager.IsInitialized)
			{
				EventManager.Instance.RemoveListener<FenceCompletedEvent>(OnFenceCompleted);
			}
		}

		private void OnFenceCompleted(FenceCompletedEvent fenceCompletedEvent)
		{
			BuildFence(fenceCompletedEvent.Origin);
		}

		private void BuildFence(FenceCorner origin)
		{
			FenceCorner currentCorner = origin;
			
			do
			{
				Vector3 delta = currentCorner.Child.CachedTransform.position - currentCorner.CachedTransform.position;
				delta.y = 0;

				float distance = delta.magnitude;

				for (int i = 0; i < distance; i++)
				{
					Vector3 spawnPosition = (currentCorner.transform.position + (i + 0.5f) * delta.normalized) -
											new Vector3(0, 0.5f, 0);

					if (Physics.Raycast(new Ray(spawnPosition + Vector3.up, Vector3.down), out RaycastHit hitInfo))
					{
						spawnPosition.y = hitInfo.point.y;
					}

					fences.Add(Instantiate(fencePrefab,
						spawnPosition,
						Quaternion.LookRotation(-delta)));
				}

				FenceCorner nextCorner = currentCorner.Child;
				Destroy(currentCorner.gameObject);
				currentCorner = nextCorner;
				
			} while (currentCorner != null && currentCorner != origin);
			
			//Invoke(nameof(DestroyFence), timeUntilFenceDisappear);
		}

		private void DestroyFence()
		{
			foreach (GameObject fence in fences)
			{
				Destroy(fence);
			}
			
			fences.Clear();
		}
	}
}