using UnityEngine;

namespace JoystickData
{
	public static class JoystickButtonHandler
	{
		public static bool IsShoulderButtonPressed(uint joystickNumber)
		{
			return Input.GetAxis($"RShoulderButton_Joystick_{joystickNumber}") > 0;
		}
	}
}