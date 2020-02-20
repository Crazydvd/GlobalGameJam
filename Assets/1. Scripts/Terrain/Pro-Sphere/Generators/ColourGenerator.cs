using UnityEngine;
using Biome = ColorSettings.BiomeColourSettings.Biome;

public class ColourGenerator
{
	private const int textureResolution = 50;
	private ColorSettings colorSettings;
	private Texture2D texture;



	public void UpdateSettings(ColorSettings colorSettings)
	{
		this.colorSettings = colorSettings;
		if (texture == null || texture.height != colorSettings.biomeColourSettings.biomes.Length)
		{
			texture = new Texture2D(textureResolution, colorSettings.biomeColourSettings.biomes.Length);
		}
	}

	/// <summary>
	/// Inform the shader on the minimum and maximum height of the terrain.
	/// </summary>
	public void UpdateElevation(MinMax elevationMinMax)
	{
		colorSettings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.MinimumElevation, elevationMinMax.MaximumElevation));
	}

	public float BiomePercentFromPoint(Vector3 pointOnUnitSphere)
	{
		float heightPercent = (pointOnUnitSphere.y + 1) / 2f;
		float biomeIndex = 0;
		int numBiomes = colorSettings.biomeColourSettings.biomes.Length;

		for (int i = 0; i < numBiomes; i++)
		{
			if (colorSettings.biomeColourSettings.biomes[i].startHeight < heightPercent)
			{
				biomeIndex = i;
			}
			else
			{
				break;
			}
		}
		return biomeIndex / Mathf.Max(1, numBiomes - 1);
	}

	public void UpdateColors()
	{
		Color[] colours = new Color[texture.width * texture.height];
		int colorIndex = 0;

		foreach (Biome biome in colorSettings.biomeColourSettings.biomes)
		{
			for (int i = 0; i < textureResolution; i++)
			{
				Color gradientCol = biome.gradient.Evaluate(i / (textureResolution - 1f));
				Color tintCol = biome.tint;
				colours[colorIndex] = gradientCol * (1 - biome.tintPercent) + tintCol * biome.tintPercent; //Color.Lerp(gradientCol, tintCol, biome.tintPercent);
				++colorIndex;
			}
		}

		texture.SetPixels(colours);
		texture.Apply();
		colorSettings.planetMaterial.SetTexture("_texture", texture);
	}
}
