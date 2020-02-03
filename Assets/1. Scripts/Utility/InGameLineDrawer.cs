using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
	public static class InGameLineDrawer
	{
		public static void DrawLine(Vector3 begin, Vector3 end, Color color)
		{
			GL.Begin(GL.LINES);
			
			GL.Color(color);
			GL.Vertex3(begin.x, begin.y, begin.z);
			
			GL.Color(color);
			GL.Vertex3(end.x, end.y, end.z);
			
			GL.End();
		}

		public static void DrawLines(IEnumerable<Vector3> vertices, Vector4 color)
		{
			GL.Begin(GL.LINES);

			foreach (Vector3 vertex in vertices)
			{
				GL.Color(color);
				GL.Vertex3(vertex.x, vertex.y, vertex.z);
			}
			
			GL.End();
		}
	}
}