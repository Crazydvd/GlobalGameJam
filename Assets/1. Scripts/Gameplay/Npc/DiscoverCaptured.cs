using System.Collections.Generic;
using System.Linq;
using Events.GameplayEvents;
using Fence;
using Internet;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.Npc
{
	public class DiscoverCaptured : BetterMonoBehaviour
	{
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