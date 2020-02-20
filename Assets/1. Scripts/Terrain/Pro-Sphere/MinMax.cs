using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax
{
	public float MinimumElevation { get; private set; } = float.MaxValue;
	public float MaximumElevation { get; private set; } = float.MinValue;

	public void AddValue(float value)
	{
		if (value > MaximumElevation)
		{
			MaximumElevation = value;
		}

		if (value < MinimumElevation)
		{
			MinimumElevation = value;
		}
	}
}
