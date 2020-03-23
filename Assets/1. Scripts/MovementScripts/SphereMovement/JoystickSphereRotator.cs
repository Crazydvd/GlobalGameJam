using JoystickData;
using UnityEngine;
using VDFramework;

namespace MovementScripts.SphereMovement
{
	public class JoystickSphereRotator : BetterMonoBehaviour
	{
		private uint joystickNumber;

		private void Awake()
		{
			joystickNumber = gameObject.GetComponent<JoystickNumber>();
		}

		private void LateUpdate()
		{
			RotateTowardsJoystickDirection();
		}

		private void RotateTowardsJoystickDirection()
		{
			Vector3 direction = JoystickInput.GetAxes(joystickNumber);

			if (Mathf.Approximately(direction.magnitude, 0))
			{
				return;
			}

			Transform parent = CachedTransform.parent;
			CachedTransform.rotation = Quaternion.LookRotation(parent.TransformDirection(direction), parent.up);
		}
	}
}