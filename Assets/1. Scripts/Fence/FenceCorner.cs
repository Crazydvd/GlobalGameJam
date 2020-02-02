using System.Collections.Generic;
using System.Linq;
using Events.GameplayEvents;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.EventSystem;
using Vector3 = UnityEngine.Vector3;

namespace Fence
{
	public class FenceCorner : BetterMonoBehaviour
	{
		public float ConnectionRadius = 5.0f;

		public FenceCorner Parent { get; private set; }
		public FenceCorner Child { get; private set; }

		private void Start()
		{
			CheckForPossibleConnections();
		}

		private void CheckForPossibleConnections()
		{
			// Get all FenceCorners
			FenceCorner[] allFenceCorners = FindObjectsOfType<FenceCorner>();

			// Filter out ourselves and everyone that is outside our reach
			IEnumerable<FenceCorner> fenceCornersInReach = allFenceCorners.Where(corner => corner != this).Where(
				corner =>
					Vector3.Distance(corner.CachedTransform.position, CachedTransform.position) <= ConnectionRadius);

			// Filter out everyone that already has a parent set 
			fenceCornersInReach = fenceCornersInReach.Where(corner => corner.Parent == null);

			// Find the closest corner
			FenceCorner closestFenceCorner = null;
			float closestDistance = float.PositiveInfinity;
			foreach (FenceCorner fenceCorner in fenceCornersInReach)
			{
				float distanceToCorner =
					Vector3.Distance(fenceCorner.CachedTransform.position, CachedTransform.position);

				if (!(distanceToCorner < closestDistance))
				{
					continue;
				}

				closestDistance = distanceToCorner;
				closestFenceCorner = fenceCorner;
			}

			// Check if the closest fence pole actually exists
			if (closestFenceCorner == null)
			{
				Debug.Log("No available corners found");
				return;
			}

			// Set our child to the closest fence corner
			// Set their parent to us
			SetChild(closestFenceCorner);

			FenceCorner child = Child;
			while (child.Child != null)
			{
				child = child.Child;

				if (child == this)
				{
					// TODO: properly calculate the sheep
					EventManager.Instance.RaiseEvent(new FenceCompletedEvent(5));
					return;
				}
			}

			child.CheckForPossibleConnections();
		}

		private void SetChild(FenceCorner other)
		{
			Child = other;
			other.Parent = this;
		}
	}
}