using Terrain.Generators;
using UnityEngine;

public static class SphereTerrain
{
	public static Mesh ConstructMesh(ShapeGenerator shapeGenerator, ColourGenerator colourGenerator, Mesh mesh, int resolution, Vector3 localY)
	{
		//Swap local axis x=y, y=z, z=x
		Vector3 localX = new Vector3(localY.y, localY.z, localY.x);
		Vector3 localZ = Vector3.Cross(localY, localX);

		//number of vertices = resolution * squared
		Vector3[] vertices = new Vector3[resolution * resolution];
		//the faces = (resolution - 1) * squared 
		//the triangles = (resolution - 1) * squared * 2
		int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
		int triIndex = 0;

	
		for (int y = 0; y < resolution; y++)
		{
			for (int x = 0; x < resolution; x++)
			{
				//Number of itterations for X loop + the total amount of Y loops * row of vertices (resolution)
				int i = x + y * resolution;
				Vector2 percent = new Vector2(x, y) / (resolution - 1);
				Vector3 pointOnUnitCube = localY + (percent.x - .5f) * 2 * localX + (percent.y - .5f) * 2 * localZ;

				// Normalize cube into sphere
				Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                float unscaledElevation = shapeGenerator.CalculateUnscaledElevation(pointOnUnitSphere);
                vertices[i] = pointOnUnitSphere * shapeGenerator.GetScaledElevation(unscaledElevation);

                	//Calculate the row of triangles 
				if (x != resolution - 1 && y != resolution - 1)
				{
					triangles[triIndex] = i;
					triangles[triIndex + 1] = i + resolution + 1;
					triangles[triIndex + 2] = i + resolution;

					triangles[triIndex + 3] = i;
					triangles[triIndex + 4] = i + 1;
					triangles[triIndex + 5] = i + resolution + 1;
					triIndex += 6;
				}
			}
		}

		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
	
		return mesh;
	}
   //public static UVGenerator UVSettings (ColourGenerator colourGenerator, Mesh mesh, int resolution, Vector3 localY)
   //{
   //    //Swap local axis x=y, y=z, z=x
   //    Vector3 localX = new Vector3(localY.y, localY.z, localY.x);
   //    Vector3 localZ = Vector3.Cross(localY, localX);
   //    //number of UVs = resolution * resolution
   //    Vector2[] uv = mesh.uv;
   //    for (int y = 0; y < resolution; y++)
   //    {
   //        for (int x = 0; x < resolution; x++)
   //        {
   //            //Number of itterations for X loop + the total amount of Y loops * row of vertices (resolution)
   //            int i = x + y * resolution;
   //            Vector2 percent = new Vector2(x, y) / (resolution - 1);
   //            Vector3 pointOnUnitCube = localY + (percent.x - .5f) * 2 * localX + (percent.y - .5f) * 2 * localZ;
   //            // Normalize cube into sphere
   //            Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
   //
   //            uv[i].x = colourGenerator.BiomePercentFromPoint(pointOnUnitSphere);
   //
   //        }
   //    }
   //    return localY;
   //
   //}
}