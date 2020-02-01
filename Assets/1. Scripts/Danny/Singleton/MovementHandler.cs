using UnityEngine;
using VDUnityFramework.Singleton;

namespace Singleton
{
	public class MovementHandler : Singleton<MovementHandler>
	{
		/// <summary>
		/// Returns a vector3 containing the joystick axis values
		/// </summary>
		/// <param name="joystickNumber">The number of the joystick to get input from</param>
		/// <returns>A vector3(HorizontalAxis, 0, VerticalAxis)</returns>
		public static Vector3 GetJoystickInput(uint joystickNumber)
		{
			float horizontalAxis = Input.GetAxisRaw($"Horizontal_Joystick_{joystickNumber}");
			float verticalAxis = Input.GetAxisRaw($"Vertical_Joystick_{joystickNumber}");
		
			return new Vector3(horizontalAxis, 0.0f,  verticalAxis);
		}

		public static Vector3 GetClampedJoystickInput(uint joystickNumber, float maxLength)
		{
			Vector3 input = GetJoystickInput(joystickNumber);

			if (input.magnitude > maxLength)
			{
				return input.normalized * maxLength;
			}

			return input;
		}
	}
}