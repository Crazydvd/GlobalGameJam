using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain.Generators;
using VDFramework.Extensions;

public class SpawnFoliageOnPlanet : MonoBehaviour
{

	public GameObject[] smallFoliages;
	public GameObject[] bigFoliages;

	private MeshFilter[] meshFilters;
	
	private Planet planet;
	private ShapeGenerator shapeGenerator;

	[SerializeField]
	private int amountOfSmallFoliage = 500;
	[SerializeField]
	private int amountOfBigFoliage = 200;

	// Start is called before the first frame update
	void Start()
	{
		planet = GetComponent<Planet>();
		shapeGenerator = planet.ShapeGenerator;

		Vector3 pointOnUnitSphere = new Vector3(shapeGenerator.elevationMinMax.MinimumElevation, shapeGenerator.elevationMinMax.MaximumElevation, 0);
		meshFilters = planet.CubeMeshes;
		//vertexElevation = new Vector3 (planet.resolution, GetComponent<ShapeGenerator>().CalculateUnscaledElevation(pointOnUnitSphere));

		if (bigFoliages.Length == 0 || smallFoliages.Length == 0)
		{
			return;
		}

		SpawnSmallFoliage();
		SpawnBigFoliage();
	}

	private void SpawnSmallFoliage()
	{
		
		for (int i = 0; i < amountOfSmallFoliage; i++)
		{
			Mesh mesh = meshFilters.GetRandomItem().sharedMesh;
			Instantiate(smallFoliages.GetRandomItem(), mesh.vertices.GetRandomItem() - new Vector3(0, -0.5f, 0), Quaternion.identity);
		}
	
	}


	private void SpawnBigFoliage()
	{
		for (int i = 0; i < amountOfBigFoliage; i++)
		{
			Mesh mesh = meshFilters.GetRandomItem().sharedMesh;
			Instantiate(bigFoliages.GetRandomItem(), mesh.vertices.GetRandomItem() - new Vector3(0, -0.5f, 0), Quaternion.identity);
		}
	}
}
