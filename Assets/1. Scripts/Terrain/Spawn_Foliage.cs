using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VDFramework.Extensions;

public class Spawn_Foliage : MonoBehaviour
{

    public GameObject[] cornerFoliages;
    public GameObject[] smallFoliages;
    private Mesh mesh;

    ProceduralTerrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<ProceduralTerrain>();
        mesh = this.gameObject.GetComponent<MeshFilter>().mesh;

        if (cornerFoliages.Length == 0 || smallFoliages.Length == 0)
        {
            return;
        }
        Surrounding();
        SpawnFoliage();
    }

    private void Surrounding()
    {
        for (int i = 0; i < mesh.vertexCount; ++i)
        {
            Vector3 vertex = mesh.vertices[i];
            if (IsAtEdge(vertex))
            {
                // Will spawn a random foliage at every vertex in the mesh.
                Instantiate(cornerFoliages.GetRandomItem(), vertex - new Vector3(0, -0.5f, 0), Quaternion.identity);
            }
        }
    }
        
    public void SpawnFoliage()
    {
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            Vector3 vertex = mesh.vertices[i];
            if (!IsAtEdge(vertex))
            {
                if (vertex.y > 1)
                {
                    // Will spawn a random foliage at every vertex in the mesh.
                    Instantiate(smallFoliages.GetRandomItem(), vertex - new Vector3(0, -0.2f, 0), Quaternion.identity);
                }
            }
        }
    }

    public bool IsAtEdge(Vector3 vertex)
    {
        if (!terrain)
        {
            terrain = GetComponent<ProceduralTerrain>();
        }
        return (vertex.x == 0 || vertex.z == 0 || vertex.x == terrain.xSize - 1 || vertex.z == terrain.zSize - 1);
    }
}
