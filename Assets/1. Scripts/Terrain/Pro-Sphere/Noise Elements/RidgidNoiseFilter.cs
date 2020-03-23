using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgidNoiseFilter : INoiseFilter
{
    NoiseSettings.RidgidNoiseSettings settings;
    Noise noise = new Noise();


    public RidgidNoiseFilter(NoiseSettings.RidgidNoiseSettings settings)
    {
        this.settings = settings;
    }
    public float Evaluate(Vector3 point)
    {
        // Noise value is -1 to +1 this squishes it to 0 to 1
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            // 1-(|sin(x)|)^2 { take the absolute value of the amplitude, inverse it and return sharp edge } 
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.centre));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v * settings.weightMultiplier);

            noiseValue += v * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistance;
        }
        noiseValue = noiseValue - settings.minValue;
        return noiseValue * settings.strength;
    }
}
