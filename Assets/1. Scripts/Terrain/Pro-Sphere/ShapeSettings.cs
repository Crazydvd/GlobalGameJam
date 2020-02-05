using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius = 1;
    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer
    {
		/// <summary>
		/// Multiply the value with the first layer value
		/// </summary>
		public bool useFirstLayerAsMask;

		public bool enabled = true;
        public bool useInversedFirstLayerAsMask;
        public NoiseSettings noiseSettings;
    }
}
