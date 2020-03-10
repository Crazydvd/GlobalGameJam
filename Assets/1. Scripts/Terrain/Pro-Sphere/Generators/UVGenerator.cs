using UnityEngine;

namespace Terrain.Generators
{
    public static class UVGenerator
    {
        //TODO Make this static class accessable as peripheral to SphereTerrain.cs
        public static void UpdateUVs(ColourGenerator colorGenerator, Mesh mesh, int resolution, Vector3 localY)
        {
            //Swap local axis x=y, y=z, z=x
            Vector3 localX = new Vector3(localY.y, localY.z, localY.x);
            Vector3 localZ = Vector3.Cross(localY, localX);

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
                    Vector3 pointOnUnitCube = localY + (percent.x - .5f) * 2 * localX + (percent.y - .5f) * 2 * localZ;
                    // Normalize cube into sphere
                    Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;

                    uv[i] = new Vector2(colorGenerator.BiomePercentFromPoint(pointOnUnitSphere), 0);
                }
            }
            //return this mesh.uv to SphereTerrain.mesh.uv
            mesh.uv = uv;
        }
    }
}

