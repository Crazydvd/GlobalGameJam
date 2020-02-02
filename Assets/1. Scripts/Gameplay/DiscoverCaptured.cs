using Events.GameplayEvents;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.EventSystem;

public class DiscoverCaptured : BetterMonoBehaviour
{
	private const int layerMask = 1 << 8;

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

	private void OnFenceCompleted()
	{
		if (IsTrappedInFence())
		{
			EventManager.Instance.RaiseEvent(new SheepCapturedEvent());
			
			Destroy(this);
		}
	}

	private bool RayCast(Vector3 direction)
	{
		return Physics.Raycast(new Ray(CachedTransform.position, direction), 50, layerMask);
	}

	private bool IsTrappedInFence()
	{
		return RayCast(CachedTransform.forward) &&
			   RayCast(CachedTransform.forward + CachedTransform.right) &&
			   RayCast(CachedTransform.right) &&
			   RayCast(CachedTransform.right - CachedTransform.forward) &&
			   RayCast(-CachedTransform.forward) &&
			   RayCast(-CachedTransform.forward - CachedTransform.right) &&
			   RayCast(-CachedTransform.right) &&
			   RayCast(-CachedTransform.right + CachedTransform.forward);
	}
}