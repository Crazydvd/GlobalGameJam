using Enums;
using Fence;
using JoystickData;
using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace Gameplay
{
	public class FencePlacer : BetterMonoBehaviour
	{
		[SerializeField] private GameObject FenceCorner = null;

		[SerializeField] private float fenceConnectionRadius = 5.0f;

		private uint joystickNumber;

		private void Start()
		{
			joystickNumber = GetComponent<JoystickNumber>();
		}

		private void Update()
		{	
			if (JoystickButtonHandler.Instance.GetButtonDown(joystickNumber, JoystickButton.RightShoulderButton))
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

			corner.ConnectionRadius = fenceConnectionRadius;
		}
	}
}