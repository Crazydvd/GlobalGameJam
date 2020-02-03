using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Utility;
using VDFramework.Extensions;
using VDUnityFramework.Singleton;

namespace JoystickData
{
	public class JoystickButtonHandler : Singleton<JoystickButtonHandler>
	{
		public bool GetButtonDown(uint joystickNumber, JoystickButton button)
		{
			return !ButtonDataHandler.IsButtonPressedLastFrame(joystickNumber, button)
				   && GetButton(joystickNumber, button);
		}

		public bool GetButtonUp(uint joystickNumber, JoystickButton button)
		{
			return ButtonDataHandler.IsButtonPressedLastFrame(joystickNumber, button)
				   && !GetButton(joystickNumber, button);
		}

		public bool GetButton(uint joystickNumber, JoystickButton button)
		{
			return Input.GetAxis(
					   $"{JoystickButtonToStringConverter.GetString(button)}{joystickNumber}") > 0;
		}

		private void LateUpdate()
		{
			ButtonDataHandler.UpdateData();
		}


		//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
		// 						ButtonDataHandler
		//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
		private static class ButtonDataHandler
		{
			private static readonly List<Dictionary<JoystickButton, bool>> buttonDataPerJoystick =
				new List<Dictionary<JoystickButton, bool>>();

			public static int JoystickCount => buttonDataPerJoystick.Count;

			public static bool IsButtonPressedLastFrame(uint joystickNumber, JoystickButton button)
			{
				return EnsureKeyIsPresent(joystickNumber, button);
			}

			/// <returns>The value of the key</returns>
			private static bool EnsureKeyIsPresent(uint joystickNumber, JoystickButton button)
			{
				// Joysticks start counting at 1, while index starts at 0
				int joystickIndex = (int) joystickNumber - 1;

				// Check if exists in list
				if (buttonDataPerJoystick.Count < joystickNumber)
				{
					buttonDataPerJoystick.ResizeList(joystickIndex + 1);
					buttonDataPerJoystick[joystickIndex] = new Dictionary<JoystickButton, bool>
					{
						{button, false}
					};

					return false;
				}

				// Dictionary might be null
				if (buttonDataPerJoystick[joystickIndex] != null)
				{
					return buttonDataPerJoystick[joystickIndex][button];
				}

				// Add it with default value if it is null
				buttonDataPerJoystick[joystickIndex] = new Dictionary<JoystickButton, bool>
				{
					{button, false}
				};

				return false;
			}

			public static void UpdateData()
			{
				for (int joystickIndex = 0; joystickIndex < JoystickCount; joystickIndex++)
				{
					Dictionary<JoystickButton, bool> dictionaryPerJoystick = buttonDataPerJoystick[joystickIndex];

					for (int i = 0; i < dictionaryPerJoystick.Count; i++)
					{
						JoystickButton button = (JoystickButton) i;

						buttonDataPerJoystick[joystickIndex][button] = IsButtonPressed(joystickIndex, button);
					}
				}

				// Local function
				bool IsButtonPressed(int joystickIndex, JoystickButton button)
				{
					uint joystickNumber = (uint) joystickIndex + 1;
					
					return Instance.GetButton(joystickNumber, button);
				}
			}
		}
	}
}