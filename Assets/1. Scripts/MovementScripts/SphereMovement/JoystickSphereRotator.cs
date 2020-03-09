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

			if (direction.magnitude > 0)
			{
				CachedTransform.rotation = Quaternion.LookRotation(CachedTransform.parent.TransformDirection(direction), CachedTransform.parent.up);
			}
		}
	}
}