using System;
using JoystickData;
using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace Gameplay
{
	public class FencePlacer : BetterMonoBehaviour
	{
		[SerializeField] private GameObject FenceCorner = null;

		private uint joystickNumber;

		private void Start()
		{
			joystickNumber = GetComponent<JoystickNumber>();
		}

		private void Update()
		{
			if (!JoystickButtonHandler.Instance.IsRightShoulderButtonPressed(joystickNumber))
			{
				return;
			}

			Vector3 spawnPosition = CachedTransform.position - CachedTransform.forward * 1.5f;

			if (Physics.Raycast(new Ray(spawnPosition + Vector3.up, Vector3.down), out RaycastHit hitInfo))
			{
				spawnPosition = hitInfo.point;
			}
				
			Instantiate(FenceCorner, spawnPosition, Quaternion.identity);
		}
	}
}