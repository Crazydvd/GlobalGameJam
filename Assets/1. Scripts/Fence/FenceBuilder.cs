using Events.GameplayEvents;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.EventSystem;

namespace Fence
{
	public class FenceBuilder : BetterMonoBehaviour
	{
		[SerializeField] private GameObject fence;

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

		private void BuildFence(FenceCorner corner)
		{
			FenceCorner currentCorner = corner;

			do
			{
				Vector3 delta = currentCorner.Child.CachedTransform.position - currentCorner.CachedTransform.position;
				delta.y = 0;

				float distance = delta.magnitude;

				for (int i = 0; i < distance; i++)
				{
					Vector3 spawnPosition = (currentCorner.transform.position + (i + 0.5f) * delta.normalized) -
											new Vector3(0, 0.5f, 0);

					spawnPosition.y = 0;

					Instantiate(fence,
						spawnPosition,
						Quaternion.LookRotation(delta));
				}

				currentCorner.GetComponent<MeshRenderer>().enabled = false;

				currentCorner = currentCorner.Child;
			} while (currentCorner != null && currentCorner != corner);
		}
	}
}