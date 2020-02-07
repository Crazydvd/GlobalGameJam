using JoystickData;
using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace MovementScripts.PlaneMovement
{
	public class JoystickRotator : BetterMonoBehaviour
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
				if (Physics.Raycast(new Ray(CachedTransform.position, -CachedTransform.up), out RaycastHit hitInfo))
				{
					CachedTransform.rotation = Quaternion.LookRotation(direction, hitInfo.normal);
				}
			}
		}
	}
}