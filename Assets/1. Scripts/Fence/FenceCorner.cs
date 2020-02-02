using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using Vector3 = UnityEngine.Vector3;

namespace Fence
{
	public class FenceCorner : BetterMonoBehaviour
	{
		public float ConnectionRadius = 5.0f;

		public FenceCorner Parent { get; private set; }

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
			if (closestFenceCorner != null)
			{
				SetAsChild(closestFenceCorner);
			}
			else
			{
				Debug.Log("No available corners found");
			}
		}

		private void SetAsChild([NotNull] FenceCorner other)
		{
			other.Parent = this;
		}
	}
}