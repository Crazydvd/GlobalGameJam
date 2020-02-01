using UnityEngine;

namespace JoystickData
{
	public static class MovementHandler
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
			return Vector3.ClampMagnitude(GetJoystickInput(joystickNumber), maxLength);
		}
	}
}