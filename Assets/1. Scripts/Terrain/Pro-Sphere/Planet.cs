using Terrain.Generators;
using UnityEngine;
using VDFramework;

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
       
    [HideInInspector]
    private MeshFilter[] cubefaces;

	private void Start()
	{
		GeneratePlanet();
	}

	private void Initialize()
    {
		shapeGenerator.UpdateSettings(shapeSettings);
        colourGenerator.UpdateSettings(colorSettings);
	
		if (cubefaces == null || cubefaces.Length == 0)
        {
            cubefaces = new MeshFilter[6];
        }

        for (int i = 0; i < 6; i++)
        {
            if (cubefaces[i] == null)
            {
                FaceRenderMask thisRenderMask = (FaceRenderMask) i + 1;
                GameObject CubeFace = new GameObject($"CubeFace {thisRenderMask}");
                CubeFace.transform.parent = CachedTransform;

				CubeFace.AddComponent<MeshRenderer>();
				CubeFace.AddComponent<MeshCollider>();

				cubefaces[i] = CubeFace.AddComponent<MeshFilter>();
                cubefaces[i].sharedMesh = new Mesh();
            }
			cubefaces[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;
			bool shouldRenderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            cubefaces[i].gameObject.SetActive(shouldRenderFace);
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
            if (cubefaces[i].gameObject.activeSelf)
            {
				// Generate terrain for every face
                cubefaces[i].GetComponent<MeshCollider>().sharedMesh = SphereTerrain.ConstructMesh(shapeGenerator,colourGenerator, cubefaces[i].sharedMesh, resolution, directions[i]);
            }
        }
        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    private void GenerateColors()
    {
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        colourGenerator.UpdateColors();
        for (int i = 0; i < 6; i++)
        {
            if (cubefaces[i].gameObject.activeSelf)
            {
				//UVGenerator.UpdateUVs(colourGenerator, cubefaces[i].sharedMesh, resolution, directions[i]);
            }
        }     
		
    }
}
