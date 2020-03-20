using System.Collections.Generic;
using Terrain.Generators;
using UnityEngine;
using VDFramework.Extensions;

public class SheepSpawn : MonoBehaviour
{
	public GameObject sheep;
	private Spawn_Foliage foliage;
	private Planet planet;
	private ShapeGenerator shapeGenerator;
	private MeshFilter[] meshFilters;

	// Start is called before the first frame update
	private void Start()
	{
		planet = GetComponent<Planet>();
		foliage = GetComponent<Spawn_Foliage>();
		shapeGenerator = planet.ShapeGenerator;

		//TODO: instead of doing this logic for everything that you want to spawn
		// just do it once in planet (AFTER GENERATION)
		meshFilters = GetComponentsInChildren<MeshFilter>();
		for (int i = 0; i < meshFilters.Length; i++)
		{
		planet.SpawnOnSurface(meshFilters);

		}

		Crusher();
	}

	private void Crusher()
	{
		List<Vector3> filteredList = new List<Vector3>();
		foreach (MeshFilter meshFilter in meshFilters)
		{
			Mesh mesh = meshFilter.sharedMesh;

			for (int i = 0; i < mesh.vertexCount; i++)
			{
				Vector3 vertex = mesh.vertices[i];
				//if (!foliage.IsAtEdge(vertex))
				{						
					if (shapeGenerator.CalculateUnscaledElevation(vertex) > 0f)
					{
						// Will spawn a random foliage at every vertex in the mesh.
						//Instantiate(sheep, transform.position, Quaternion.identity);
						filteredList.Add(vertex);
					}
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
