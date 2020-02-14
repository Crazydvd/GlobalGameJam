using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Fence;
using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace Utility
{
	public class ShowFencesWithinReach : BetterMonoBehaviour
	{
		[SerializeField] private float radius = 5.0f;
		//TODO: take reference of empty gameobject spawn point for fences

		private LineGenerator lastLine;

		[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
		private void Update()
		{
			if (lastLine)
			{
				lastLine.DeleteLast();
				lastLine = null;
			}
			
			//TODO: split into methods
			IEnumerable<FenceCorner> allFenceCorners = FindObjectsOfType<FenceCorner>().ToList();

			if (allFenceCorners.Any())
			{
				IEnumerable<FenceCorner> cornersInReach =
					allFenceCorners.Where(corner => DistanceToObject(corner) <= radius);

				
				if (!cornersInReach.Any())
				{
					return;
				}

				FenceCorner closest = GetClosestCorner(cornersInReach);

				if (closest)
				{
					DrawLineToUsFrom(closest);

					FenceCorner root = closest.Root;

					if (DistanceToObject(root) < radius)
					{
						DrawLineToUsFrom(root);
					}
				}
			}
		}

		private void DrawLineToUsFrom(FenceCorner fenceCorner)
		{
			lastLine = fenceCorner.GetComponent<LineGenerator>();
			lastLine.AddVertexToEnd(CachedTransform.position - CachedTransform.forward * 1.5f);
        }
      
		private float DistanceToObject(BetterMonoBehaviour behaviour)
		{
			if (behaviour == null)
			{
				return int.MaxValue;
			}

			return Vector3.Distance(behaviour.CachedTransform.position,
				CachedTransform.position - CachedTransform.forward * 1.5f);
		}

		private FenceCorner GetClosestCorner(IEnumerable<FenceCorner> corners)
		{
			FenceCorner closestFenceCorner = null;
			float closestDistance = float.PositiveInfinity;
			foreach (FenceCorner fenceCorner in corners)
			{
				if (fenceCorner.Parent != null)
				{
					continue;
				}

				float distanceToCorner =
					Vector3.Distance(fenceCorner.CachedTransform.position,
						CachedTransform.position - CachedTransform.forward * 1.5f);

				if (!(distanceToCorner < closestDistance))
				{
					continue;
				}

				closestDistance = distanceToCorner;
				closestFenceCorner = fenceCorner;
			}

			return closestFenceCorner;
		}
		
	}
}