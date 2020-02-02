﻿using JoystickData;
using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace Gameplay
{
	public class FencePlacer : BetterMonoBehaviour
	{
		[SerializeField] private GameObject FenceCorner;

		private void Update()
		{
			if (JoystickButtonHandler.IsShoulderButtonPressed(GetComponent<JoystickNumber>()))
			{
				Instantiate(FenceCorner, CachedTransform.position - CachedTransform.forward, Quaternion.identity);
			}
		}
	}
}