using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VDFramework.Extensions;

public class SheepSpawn : MonoBehaviour
{
    public GameObject sheep;

    Spawn_Foliage foliage;

    Mesh mesh;
    
    // Start is called before the first frame update
    void Start()
    {
        foliage = GetComponent<Spawn_Foliage>();

        mesh = GetComponent<MeshFilter>().mesh;

        Crusher();
   
    }
       
    void Crusher()
    {
        List<Vector3> filteredList = new List<Vector3>();

        for (int i = 0; i < mesh.vertexCount; i++)
        {
            Vector3 vertex = mesh.vertices[i];
            if (!foliage.IsAtEdge(vertex))
            {
                if (vertex.y < 1)
                {
                    // Will spawn a random foliage at every vertex in the mesh.
                    //Instantiate(sheep, transform.position, Quaternion.identity);
                    filteredList.Add(vertex);
                }
            }
        }

        int amountOfSheep = 12;

        for (int i = 0; i < amountOfSheep; i++)
        {
            Instantiate(sheep, filteredList.GetRandomItem(), Quaternion.identity);
        }
    }


    //TODO: spawn dead sheep
}
