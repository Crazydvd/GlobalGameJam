using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VDFramework.Extensions;

public class PlayerSpawn : MonoBehaviour
{

    public GameObject Player1;
    public GameObject Player2;
    Spawn_Foliage foliage;

    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        foliage = GetComponent<Spawn_Foliage>();

        mesh = GetComponent<MeshFilter>().mesh;

        Sheperd();

    }
    
    void Sheperd()
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
        int AllPlayers = 2;
        for (int i = 0; i < AllPlayers; i++)
        {
            Instantiate(Player1, filteredList.GetRandomItem(), Quaternion.identity);
            Instantiate(Player2, filteredList.GetRandomItem(), Quaternion.identity);
        }
    }
}
