using System.Collections.Generic;
using Enums;
using UnityEngine;
using VDFramework.Extensions;
using VDUnityFramework.Singleton;
using ButtonManager = Utility.JoystickButtonManager;

namespace JoystickData
{
	public class JoystickButtonHandler : Singleton<JoystickButtonHandler>
	{
		public bool IsRightShoulderButtonPressed(uint joystickNumber)
		{
			return !ButtonDataHandler.IsButtonPressedLastFrame(joystickNumber, JoystickButton.RightShoulderButton)
				   && IsRightShoulderButtonDown(joystickNumber);
		}

		public bool IsRightShoulderButtonUp(uint joystickNumber)
		{
			return ButtonDataHandler.IsButtonPressedLastFrame(joystickNumber, JoystickButton.RightShoulderButton)
				   && !IsRightShoulderButtonDown(joystickNumber);
		}

		public bool IsRightShoulderButtonDown(uint joystickNumber)
		{
			return Input.GetAxis(
					   $"{ButtonManager.GetString(JoystickButton.RightShoulderButton)}{joystickNumber}") > 0;
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
				return EnsureKeyIsPresent(joystickNumber, button);// buttonDataPerJoystick[(int) joystickNumber - 1][button];
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
					return Input.GetAxis($"{ButtonManager.GetString(button)}{joystickIndex + 1}") > 0;
				}
			}
		}
	}
}