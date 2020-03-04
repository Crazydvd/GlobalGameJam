using System.Collections.Generic;
using System.Linq;
using Events.GameplayEvents;
using UnityEngine;
using Utility;
using VDFramework;
using VDFramework.EventSystem;
using VDFramework.UnityExtensions;

namespace Fence
{
	public class FenceCorner : BetterMonoBehaviour
	{
		public FenceCorner Parent { get; private set; }
		public FenceCorner Child { get; private set; }

		public FenceCorner End
		{
			get
			{
				FenceCorner end = this;

				while (end.Parent != null && end.Parent != this)
				{
					end = end.Parent;
				}

				return end.Parent == this ? this : end;
			}
		}

		public FenceCorner Root
		{
			get
			{
				FenceCorner origin = this;

				while (origin.Child != null && origin.Child != this)
				{
					origin = origin.Child;
				}

				return origin.Child == this ? this : origin;
			}
		}

		public float ConnectionRadius = 5.0f;

		private LineGenerator lineGenerator;

		private void Start()
		{
			lineGenerator = gameObject.EnsureComponent<LineGenerator>();

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
				if (fenceCorner == Parent)
				{
					continue;
				}

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
				return;
			}

			// Set our child to the closest fence corner
			// Set their parent to us
			SetChild(closestFenceCorner);

			if (Root == this)
			{
				EventManager.Instance.RaiseEvent(new FenceCompletedEvent(this));
				return;
			}

			// Make the beginning check for connections (to detect the end)
			Root.CheckForPossibleConnections();

			EventManager.Instance.RaiseEvent(new FenceCornerPlacedEvent(this));
		}

		private void SetChild(FenceCorner other)
		{
			Child = other;
			other.Parent = this;

			lineGenerator.RemoveLine();
			lineGenerator.AddVertexToEnd(Child.transform);
			lineGenerator.AddSelf();
		}

		private void OnDestroy()
		{
			Parent = null;
			Child = null;
		}
	}
}