using Events.GameplayEvents;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.EventSystem;

namespace Fence
{
	public class FenceBuilder : BetterMonoBehaviour
	{
		[SerializeField] private GameObject fence;
		
		private void Start()
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
			Vector3 delta = corner.Child.CachedTransform.position - corner.CachedTransform.position;
			float distance = delta.magnitude;
			
			for (int i = 0; i < distance; i++)
			{
				Instantiate(fence, corner.transform.position + i * 0.5f * delta.normalized, Quaternion.identity);
			}
		}		
		

		///YRotation = vec3.Angle(Vec3.forward, transform.pos - end.pos)
		/// if (end.pos.x - transform.pos.x > 0.0f)
		/// {
		/// 	Yrotation *= -1.0f;
		/// }
		///
		/// plank.transform.localEulerAngles = new Vec3(0, YRotation, 0);
	}
}