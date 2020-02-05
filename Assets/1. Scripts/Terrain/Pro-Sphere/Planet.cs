using UnityEngine;
using VDUnityFramework.BaseClasses;

public class Planet : BetterMonoBehaviour
{
	public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
	public FaceRenderMask faceRenderMask;

	[Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    private ShapeGenerator shapeGenerator;

    [SerializeField] private Material cubeFaceMaterial;

    [HideInInspector]
    private MeshFilter[] meshFilters;

    private void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapeSettings);
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                FaceRenderMask thisRenderMask = (FaceRenderMask) i + 1;
                GameObject CubeFace = new GameObject($"CubeFace {thisRenderMask}");
                CubeFace.transform.parent = CachedTransform;

                CubeFace.AddComponent<MeshRenderer>().sharedMaterial = cubeFaceMaterial;
				CubeFace.AddComponent<MeshCollider>();

				meshFilters[i] = CubeFace.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

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

    private void GenerateMesh()
    {
		Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

		for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                meshFilters[i].GetComponent<MeshCollider>().sharedMesh = SphereTerrain.ConstructMesh(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            }
        }
    }

    private void GenerateColors()
    {
		if (meshFilters[0] != null)
		{
			meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.ColorOfPlanet;
		}

		//TODO: add back later
        //foreach (MeshFilter meshFilter in meshFilters)
        //{
        //    meshFilter.GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.ColorOfPlanet;
        //}
    }
}
