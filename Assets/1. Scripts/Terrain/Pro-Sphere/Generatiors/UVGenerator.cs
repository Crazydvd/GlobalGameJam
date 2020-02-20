using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UVGenerator 
{
    //TODO Make this static class accessable as peripheral to SphereTerrain.cs
    public static void UpdateUVs(ColourGenerator colorGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
        //Swap local axis x=y, y=z, z=x
        Vector3 axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        Vector3 axisB = Vector3.Cross(localUp, axisA);

        //number of vertices = resolution * squared
       Vector3[] vertices = new Vector3[resolution * resolution];
      /*________________________________________From_The_Tutorial_________________________________________________________ */
        //number of UVs = resolution * resolution
        Vector2[] uv = new Vector2[resolution * resolution];

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

                uv[i] = new Vector2(colorGenerator.BiomePercentFromPoint(pointOnUnitSphere),0);
            }
        }
        //return this mesh.uv to SphereTerrain.mesh.uv
       mesh.uv = uv;
    }
}
