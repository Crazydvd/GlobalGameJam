using Terrain.Generators;
using UnityEngine;

public class TerrainFaceMesh
{
	ShapeGenerator shapeGenerator;
	int resolution;
	Vector3 localY;
	Vector3 localX;
	Vector3 localZ;
	Mesh mesh;

	public TerrainFaceMesh(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localY)
	{
		this.shapeGenerator = shapeGenerator;
		this.resolution = resolution;
		this.localY = localY;
		this.mesh = mesh;

		//Swap local axis x=y, y=z, z=x
		localX = new Vector3(localY.y, localY.z, localY.x);
		localZ = Vector3.Cross(localY, localX);
	}
	public Mesh ConstructMesh()
	{
		//number of vertices = resolution * squared
		Vector3[] vertices = new Vector3[resolution * resolution];
		//the faces = (resolution - 1) * squared 
		//the triangles = (resolution - 1) * squared * 2
		int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
		int triIndex = 0;

		Vector2[] uv = (mesh.uv.Length == vertices.Length) ? mesh.uv : new Vector2[vertices.Length];

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
				uv[i].y = unscaledElevation;

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
		mesh.uv = uv;

		return mesh;
	}

	public void UpdateUVs(ColourGenerator colourGenerator)
	{
		//number of UVs = resolution * resolution
		Vector2[] uv = mesh.uv;

		if (uv.Length == 0)
		{
			bool a = true;
		}

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

				//TODO: figure out why the array has length 0 (keep in mind, this is not always the case)
				uv[i].x = colourGenerator.BiomePercentFromPoint(pointOnUnitSphere);
			}
		}
		mesh.uv = uv;
	}
}