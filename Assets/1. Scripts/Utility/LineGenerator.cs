using System;
using System.Collections.Generic;
using UnityEngine;
using VDFramework;
using VDFramework.UnityExtensions;

namespace Utility
{
	public class LineGenerator : BetterMonoBehaviour
	{
		private LineRenderer lineRenderer;

		private readonly Dictionary<int, Transform> objectPositions = new Dictionary<int, Transform>();
		private readonly Dictionary<int, Vector3> vectorPositions = new Dictionary<int, Vector3>();

		private int positionCount;

		private void Awake()
		{
			lineRenderer = this.EnsureComponent<LineRenderer>();
		}

		public void Update()
		{
			DrawLine();
		}

		public void AddSelf()
		{
			AddVertexToEnd(CachedTransform);
		}

		public void DrawLine(IEnumerable<Vector3> positions)
		{
			foreach (Vector3 position in positions)
			{
				AddVertexToEnd(position);
			}
		}

		public void DrawLine(IEnumerable<Transform> positions)
		{
			foreach (Transform position in positions)
			{
				AddVertexToEnd(position);
			}
		}

		public void AddVertexToEnd(Vector3 position)
		{
			vectorPositions[positionCount++] = position;
		}

		public void AddVertexToEnd(Transform position)
		{
			objectPositions[positionCount++] = position;
		}

		public void DeleteLastVertex()
		{
			if (positionCount == 1)
			{
				RemoveLine();
			}

			vectorPositions.Remove(positionCount);
			objectPositions.Remove(positionCount);

			positionCount -= 1;
		}
		
		public void RemoveIndex(int index)
		{
			vectorPositions.Remove(index);
			objectPositions.Remove(index);

			ReduceEveryIndexByOne(vectorPositions, index);
			ReduceEveryIndexByOne(objectPositions, index);
		}

		public void RemoveLine()
		{
			lineRenderer.positionCount = 0;
			positionCount = 0;

			vectorPositions.Clear();
			objectPositions.Clear();
		}

		private void DrawLine()
		{
			lineRenderer.positionCount = positionCount;

			Vector3[] positions = new Vector3[positionCount];

			for (int i = 0; i < positionCount; i++)
			{
				positions[i] = GetPositionForIndex(i);
			}

			lineRenderer.SetPositions(positions);
		}

		private Vector3 GetPositionForIndex(int index)
		{
			if (vectorPositions.ContainsKey(index))
			{
				return vectorPositions[index];
			}

			if (objectPositions.ContainsKey(index))
			{
				return objectPositions[index].position;
			}

			// Should never happen
			throw new Exception($"position {index} is not stored in internal data");
		}

		private static void ReduceEveryIndexByOne<TValueType>(IDictionary<int, TValueType> dictionary, int startIndex)
		{
			foreach (KeyValuePair<int, TValueType> entry in dictionary)
			{
				int key = entry.Key;
				int newKey = key - 1;

				if (key > startIndex)
				{
					dictionary[newKey] = dictionary[key];
					dictionary.Remove(key);
				}
			}
		}
	}
}