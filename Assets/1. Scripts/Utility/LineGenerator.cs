using System.Collections.Generic;
using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace Utility
{
	public static class LineGenerator
	{
       // private LineRenderer f_lineRenderer = null;
       // private Fence.FenceCorner availableConnection;


        public static void LineRenderer(LineRenderer f_lineRenderer, Vector3 start, Vector3 end, Vector4 color)
        {
            if (f_lineRenderer != null)
            {
                
                //f_lineRenderer = availableConnection.gameObject.GetComponent<LineRenderer>();
                f_lineRenderer.useWorldSpace = true;
                //f_lineRenderer.positionCount = GameObject.
                f_lineRenderer.startWidth = 20.1f;
                f_lineRenderer.endWidth = 20.1f;
                
               // f_lineRenderer.material = new Material(Shader.Find("Universal Rendering Pipeline/Simple Lit"));
                f_lineRenderer.startColor = Color.blue; 
                f_lineRenderer.endColor = Color.clear;
            }
        }


		public static void DrawLine(Vector3 begin, Vector3 end, Vector4 color)
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