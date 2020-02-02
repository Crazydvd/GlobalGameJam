using System.Collections.Generic;
using System.Linq;
using Events.GameplayEvents;
using Fence;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.EventSystem;
using Internet;

namespace Gameplay
{
	public class DiscoverCaptured : BetterMonoBehaviour
	{
		private int layerMask = 1 << 8;

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
			if (IsTrappedInFence(fenceCompletedEvent.Origin))
			{
				EventManager.Instance.RaiseEvent(new SheepCapturedEvent());

				Destroy(gameObject);
			}
		}

		private bool RayCast(Vector3 direction)
		{
				bool hit = Physics.Raycast(new Ray(CachedTransform.position, direction), int.MaxValue,
				~layerMask);
				
				
				Debug.DrawLine(CachedTransform.position, direction, Color.yellow);

				return hit;
		}

		private bool IsTrappedInFence(FenceCorner origin)
		{
			List<FenceCorner> corners = new List<FenceCorner>();
			FenceCorner currentCorner = origin;
			corners.Add(origin);

			do
			{
				currentCorner = currentCorner.Child;
				corners.Add(currentCorner);

			} while (currentCorner != origin);

			List<Vector2> polygonVertices = corners.Select(fenceCorner => new Vector2(fenceCorner.CachedTransform.position.x, fenceCorner.CachedTransform.position.z)).ToList();
			Vector2 currentPos = new Vector2(CachedTransform.position.x, CachedTransform.position.z);

			return Poly.ContainsPoint(polygonVertices.ToArray(), currentPos);
		}
	}
}