using System.Collections.Generic;
using System.Linq;
using Fence;
using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace Utility
{
	public class ShowFencesWithinReach : BetterMonoBehaviour
	{
		[SerializeField] private float radius = 5.0f;

        private Fence.FenceCorner availableConnection;


        private void OnRenderObject()
		{
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
					DrawLine(closest, Color.green);

					FenceCorner root = closest.Root;

					if (DistanceToObject(root) < radius)
					{
						DrawLine(root, Color.red);
					}
				}
			}
		}

		private void DrawLine(BetterMonoBehaviour begin, Vector4 color)
		{
			//LineGenerator.DrawLine(begin.CachedTransform.position,CachedTransform.position - CachedTransform.forward * 1.5f, color);
            LineGenerator.LineRenderer(availableConnection.lineRenderer,begin.CachedTransform.position, CachedTransform.position - CachedTransform.forward * 1.5f, color);
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