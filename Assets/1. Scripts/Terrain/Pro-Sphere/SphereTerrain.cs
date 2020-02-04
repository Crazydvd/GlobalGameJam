using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTerrain
{
    ShapeGenerator shapeGenerator;
    Planet planet;
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;
    public MeshCollider meshCollider;

    public SphereTerrain(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp, MeshCollider meshCollider)
    {
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        this.meshCollider = meshCollider;

        //Swap local axis x=y, y=z, z=x
        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        //number of vertex matrix = resolution * scquared
        Vector3[] vertices = new Vector3[resolution * resolution];
        //the faces = (resolution - 1) * scquared 
        //the triangles = (resolution - 1) * scquared * 2
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                //Number of itterations for X loop + the total amount of Y loops * row of vertices (resolution)
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;

                // Normalize cube into sphere
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);
                
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
        Vector3[] normals = mesh.normals;
        for (var i = 0; i <= normals.Length; i++)
        {
            mesh.normals = normals;
        }
                
      ///  meshCollider.sharedMesh = mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}