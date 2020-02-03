using JoystickData;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.UnityExtensions;
using UnityEngine;

namespace MovementScripts
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
			Vector3 direction = MovementHandler.GetJoystickAxes(joystickNumber); 
			
			if (direction.magnitude > 0)
			{
				CachedTransform.rotation = Quaternion.LookRotation(direction);
			}
		}
	}
}