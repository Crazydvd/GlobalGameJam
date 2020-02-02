using VDFramework.Extensions;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralTerrain : MonoBehaviour
{
    [Header("Heightmaps")]
    public Texture2D[] textures;

    [Space(20.0f)]
    public int xSize = 20;
    public int zSize = 20;

    [Range(0, 100)]
    public float scale = 5f;

    public Gradient gradient;
    public float offsetX = 100f;
    public float offsetY = 100f;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Color[] colors;

    private MeshCollider meshCollider;

    new Renderer renderer;
 
    // Start is called before the first frame update
    void Awake()
    {
        mesh = new Mesh();

        xSize = Random.Range(15, 50);
        zSize = Random.Range(15, 50);
        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);

        renderer = GetComponent<Renderer>();
     
        CreateShape();
        UpdateMesh();

        GetComponent<MeshFilter>().mesh = mesh;
        Vector3[] normals = mesh.normals;
        for (var i = 0; i <= normals.Length; i++)
        {
            mesh.normals = normals;
        }

        meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    private void OnValidate()
    {
        if (!renderer)
        {
            return;
        }

        CreateShape();
        UpdateMesh();
        meshCollider.sharedMesh = mesh;
    }

    void CreateShape()
    {
        Texture2D generatedTexture = (renderer.sharedMaterial.mainTexture as Texture2D);
        Texture2D customTexture = textures.GetRandomItem();

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];


        colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float pixelSaturation = generatedTexture.GetPixel(x, z).grayscale;

                float perlinValue = CalculateNoise(x, z);
                float y = pixelSaturation + perlinValue;
                Color color = gradient.Evaluate(perlinValue);

                colors[i] = color;

                if (customTexture)
                {
                    y *= customTexture.GetPixel(x, z).grayscale + CalculateNoise(x, z);
                }

                vertices[i] = new Vector3(x, y, z);
                ++i;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                ++vert;
                tris += 6;
            }
            ++vert;
        }
    }
    float CalculateNoise(float x, float y)
    {
        float xCoord = x / xSize * scale + offsetX;
        float yCoord = y / zSize * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return sample;

    }
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }
}
