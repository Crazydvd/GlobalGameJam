using Enums;
using Fence;
using JoystickData;
using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace Gameplay
{
	public class FencePlacer : BetterMonoBehaviour
	{
		[SerializeField] private float connectionRadius = 5.0f;

		[SerializeField] private GameObject FenceCorner = null;

		private uint joystickNumber;

		private void Start()
		{
			joystickNumber = GetComponent<JoystickNumber>();
		}

		private void Update()
		{
			if (JoystickInput.GetButtonDown(joystickNumber, JoystickButton.RightShoulderButton))
			{
				SpawnFenceCorner();
			}
		}

		private void SpawnFenceCorner()
		{
			Vector3 spawnPosition = CachedTransform.position - CachedTransform.forward * 1.5f;

			if (Physics.Raycast(new Ray(spawnPosition + Vector3.up, Vector3.down), out RaycastHit hitInfo))
			{
				spawnPosition = hitInfo.point;
			}

			FenceCorner corner = Instantiate(FenceCorner, spawnPosition, Quaternion.identity)
				.GetComponent<FenceCorner>();

			corner.ConnectionRadius = connectionRadius;
		}
	}
}