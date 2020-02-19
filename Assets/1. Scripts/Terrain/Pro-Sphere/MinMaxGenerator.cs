using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxGenerator
{ 
    public float Min { get; private set; }
    public float Max { get; private set; }

   
    ColorSettings colorSettings;
    Texture2D texture;
    const int textureResolution = 50;

    public MinMaxGenerator()
    {
        Min = float.MaxValue;
        Max = float.MinValue;
            
    }

    public void AddValue(float v)
    {
        if (v > Max)
        {
            Max = v;
        }
        if (v < Min)
        {
            Min = v;
        }
    }

    public void UpdateSettings (ColorSettings colorSettings)
    {
        this.colorSettings = colorSettings;
        if (texture == null || texture.height != colorSettings.biomeColourSettings.biomes.Length)
        {
            texture = new Texture2D(textureResolution, colorSettings.biomeColourSettings.biomes.Length);
        }
    }
    
   public void UpdateElevation (MinMaxGenerator elevationMinMax)
    {
        colorSettings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
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
        return biomeIndex / Mathf.Max (1, numBiomes - 1);
    }

    public void UpdateColors()
    {
        Color[] colours = new Color[texture.width * texture.height];
        int colorIndex = 0;
        foreach (var biome in colorSettings.biomeColourSettings.biomes)
        {
            for (int i = 0; i < textureResolution; i++)
            {
                Color gradientCol = biome.gradient.Evaluate(i / (textureResolution - 1f));
                Color tintCol = biome.tint;
                colours[colorIndex] = gradientCol * (1 - biome.tintPercent) + tintCol * biome.tintPercent;
                colorIndex++;
            }
        }
        texture.SetPixels(colours);
        texture.Apply();
        colorSettings.planetMaterial.SetTexture("_texture", texture);
    }
}
