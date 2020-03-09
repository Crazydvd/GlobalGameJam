using JoystickData;
using UnityEngine;
using VDFramework;

namespace MovementScripts.PlaneMovement
{
	public class JoystickPlaneRotator : BetterMonoBehaviour
	{
		private uint joystickNumber;

		private void Awake()
		{
			joystickNumber = gameObject.GetComponent<JoystickNumber>();
		}

		private void Update()
		{
			RotateTowardsJoystickDirection();
		}

		private void RotateTowardsJoystickDirection()
		{
			Vector3 direction = JoystickInput.GetAxes(joystickNumber);

			if (direction.magnitude > 0)
			{
				CachedTransform.rotation =
					Quaternion.LookRotation(direction,
						CachedTransform.up);
			}
		}
	}
}