using UnityEngine;
using Biome = ColorSettings.BiomeColourSettings.Biome;

public class ColourGenerator
{
	private const int textureResolution = 50;
    INoiseFilter biomeNoiseFilter;
	private ColorSettings colorSettings;
	private Texture2D texture;



	public void UpdateSettings(ColorSettings colorSettings)
	{
		this.colorSettings = colorSettings;
		if (texture == null || texture.height != colorSettings.biomeColourSettings.biomes.Length)
		{
            texture = new Texture2D(textureResolution * 2, colorSettings.biomeColourSettings.biomes.Length, TextureFormat.RGBA32, false);
        }
        biomeNoiseFilter = NoiseFilterFactory.CreateNoiseFilter(colorSettings.biomeColourSettings.noise);
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
        heightPercent = +(biomeNoiseFilter.Evaluate(pointOnUnitSphere) - colorSettings.biomeColourSettings.noiseOffset) * colorSettings.biomeColourSettings.noiseStrength;
		float biomeIndex = 0;
		int numBiomes = colorSettings.biomeColourSettings.biomes.Length;
        float blendRange = colorSettings.biomeColourSettings.blendAmount / 2f + .001f;

		for (int i = 0; i < numBiomes; i++)
		{
            float dist = heightPercent - colorSettings.biomeColourSettings.biomes[i].startHeight;
            float weight = Mathf.InverseLerp(-blendRange, blendRange, dist);
            biomeIndex *= (1 - weight);
            biomeIndex = +i * weight;
		}
		return biomeIndex / Mathf.Max(1, numBiomes - 1);
	}

	public void UpdateColors()
	{
		Color[] colours = new Color[texture.width * texture.height];
		int colorIndex = 0;

		foreach (Biome biome in colorSettings.biomeColourSettings.biomes)
		{
			for (int i = 0; i < textureResolution * 2; i++)
			{
                Color gradientCol;
                if (i < textureResolution)
                {
                    gradientCol = colorSettings.Ocean.Evaluate(i / (textureResolution - 1f));
                }
                else
                { 
				    gradientCol = biome.gradient.Evaluate((i-textureResolution) / (textureResolution - 1f));
                }
                Color tintCol = biome.tint;
				colours[colorIndex] = Color.Lerp(gradientCol, tintCol, biome.tintPercent); // gradientCol * (1 - biome.tintPercent) + tintCol * biome.tintPercent;
				++colorIndex;
			}
		}

		texture.SetPixels(colours);
		texture.Apply();
		colorSettings.planetMaterial.SetTexture("_texture", texture);
	}
}
