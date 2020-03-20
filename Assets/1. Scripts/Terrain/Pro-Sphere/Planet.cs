using Terrain.Generators;
using UnityEngine;
using VDFramework;
using VDFramework.UnityExtensions;

public class Planet : BetterMonoBehaviour
{
	public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
	public FaceRenderMask faceRenderMask;

	[Range(2, 256)]
	public int resolution = 10;
	public bool autoUpdate = true;

	public ShapeSettings shapeSettings;
	public ColorSettings colorSettings;

	private ShapeGenerator shapeGenerator = new ShapeGenerator();
	private ColourGenerator colourGenerator = new ColourGenerator();

	public ShapeGenerator ShapeGenerator => shapeGenerator;
	public ColourGenerator ColourGenerator => colourGenerator;

	[SerializeField]
	private MeshFilter[] meshFilters;
	[HideInInspector]
	private TerrainFaceMesh[] terrainFaces;

	private void Awake()
	{
		GeneratePlanet();
	}

	private void Initialize()
	{
		shapeGenerator.UpdateSettings(shapeSettings);
		colourGenerator.UpdateSettings(colorSettings);

		if (meshFilters == null || meshFilters.Length < 6)
		{
			meshFilters = new MeshFilter[6];
		}
		terrainFaces = new TerrainFaceMesh[6];

		Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

		for (int i = 0; i < 6; i++)
		{
			if (meshFilters[i] == null)
			{
				FaceRenderMask thisRenderMask = (FaceRenderMask)i + 1;
				GameObject CubeFace = new GameObject($"CubeFace {thisRenderMask}");
				CubeFace.transform.parent = CachedTransform;

				CubeFace.AddComponent<MeshRenderer>();
				CubeFace.AddComponent<MeshCollider>();

				meshFilters[i] = CubeFace.AddComponent<MeshFilter>();
				meshFilters[i].sharedMesh = new Mesh();
			}

			meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;
			terrainFaces[i] = new TerrainFaceMesh(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
			bool shouldRenderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
			meshFilters[i].gameObject.SetActive(shouldRenderFace);
		}		
	}

	public void GeneratePlanet()
	{
		Initialize();
		GenerateMesh();
		GenerateColors();
	}
	public void OnShapeSettingsUpdated()
	{
		if (autoUpdate)
		{
			Initialize();
			GenerateMesh();
		}
	}
	public void OnColorSettingsUpdated()
	{
		if (autoUpdate)
		{
			Initialize();
			GenerateColors();
		}
	}
	public void SpawnOnSurface(MeshFilter[] array)
	{
		array = GetComponentsInChildren<MeshFilter>();
		for (int i = 0, j = 0; i < array.Length; i++)
		{
			MeshFilter meshFilter = array[i];
			if (meshFilter.CompareTag("Water"))
			{
				continue;
			}
			meshFilters[j] = meshFilter;

			++j;
		}
	}

	private void GenerateMesh()
	{
		for (int i = 0; i < 6; i++)
		{
			MeshFilter filter = meshFilters[i];

			if (filter.gameObject.activeSelf)
			{
				// Generate terrain for every face 
				//meshFilters[i].GetComponent<MeshCollider>().sharedMesh = SphereTerrain.ConstructMesh(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
				filter.sharedMesh = terrainFaces[i].ConstructMesh();

				filter.EnsureComponent<MeshCollider>().sharedMesh = filter.sharedMesh;

				//TODO: cache it beforehand, this is ineffective
				// meshFilters[i].GetComponent<MeshCollider>().sharedMesh = meshFilters[i].mesh;
			}
		}
		colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
	}

	private void GenerateColors()
	{
		colourGenerator.UpdateColors();
		for (int i = 0; i < 6; i++)
		{
			if (meshFilters[i].gameObject.activeSelf)
			{
				terrainFaces[i].UpdateUVs(colourGenerator);
			}
		}
	}
}
