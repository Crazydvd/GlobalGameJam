﻿using Enums;
using UnityEngine;
using Utility;

namespace JoystickData
{
	public static class MovementHandler
	{
		/// <summary>
		/// Returns a vector3 containing the joystick axis values
		/// </summary>
		/// <param name="joystickNumber">The number of the joystick to get input from</param>
		/// <returns>A vector3(HorizontalAxis, 0, VerticalAxis)</returns>
		public static Vector3 GetJoystickAxes(uint joystickNumber)
		{
			float horizontalAxis =
				Input.GetAxisRaw($"{JoystickButtonToStringConverter.GetString(JoystickButton.HorizontalAxis)}{joystickNumber}");
			float verticalAxis = 
				Input.GetAxisRaw($"{JoystickButtonToStringConverter.GetString(JoystickButton.VerticalAxis)}{joystickNumber}");

			return new Vector3(horizontalAxis, 0.0f, verticalAxis);
		}

		public static Vector3 GetClampedJoystickAxes(uint joystickNumber, float maxLength)
		{
			return Vector3.ClampMagnitude(GetJoystickAxes(joystickNumber), maxLength);
		}
	}
}