using UnityEngine;

namespace Internet
{
	public static class Poly
	{
		public static bool ContainsPoint(Vector2[] polyPoints, Vector2 p)
		{
			int j = polyPoints.Length - 1;
			bool inside = false;
			
			for (int i = 0; i < polyPoints.Length; j = i++)
			{
				Vector2 pi = polyPoints[i];
				Vector2 pj = polyPoints[j];
				if (((pi.y <= p.y && p.y < pj.y) || (pj.y <= p.y && p.y < pi.y)) &&
					(p.x < (pj.x - pi.x) * (p.y - pi.y) / (pj.y - pi.y) + pi.x))
					inside = !inside;
			}
			return inside;
		}
	}
}