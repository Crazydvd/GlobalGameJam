using System;
using System.Collections.Generic;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.UnityExtensions;

namespace Utility
{
	public class LineGenerator : BetterMonoBehaviour
	{
		private LineRenderer lineRenderer;

		private void Awake()
		{
			lineRenderer = gameObject.EnsureComponent<LineRenderer>();
		}

		public void DrawLineBetween(Vector3 start, Vector3 end)
		{
			lineRenderer.positionCount = 2;
			
			lineRenderer.SetPosition(0, start);
			lineRenderer.SetPosition(1, end);
		}

		public void DrawLineBetween(Vector3 start, Vector3 end, Material lineMaterial)
		{
			lineRenderer.material = lineMaterial;
			DrawLineBetween(start, end);
		}

		public void AddVertexToEnd(Vector3 position)
		{
			int index = lineRenderer.positionCount;

			lineRenderer.positionCount = index + 1;
			
			lineRenderer.SetPosition(index, position);
		}

		public void DeleteLast()
		{
			if (lineRenderer.positionCount == 1)
			{
				lineRenderer.positionCount = 0;
			}
			
			lineRenderer.positionCount -= 1;
		}
	}
}