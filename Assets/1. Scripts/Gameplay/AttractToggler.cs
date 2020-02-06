using Enums;
using JoystickData;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.EventSystem;

namespace Gameplay
{
	public class AttractToggler : BetterMonoBehaviour
	{
		private uint joystickNumber;

		private bool attractEnabled = false;

		private AttractScript attractScript;
				
		private void Awake()
		{
			attractScript = GetComponent<AttractScript>();
			joystickNumber = GetComponent<JoystickNumber>();
		}

		private void Update()
		{
			if (JoystickButtonHandler.Instance.GetButtonDown(joystickNumber, JoystickButton.RightShoulderButton))
			{
				attractEnabled = !attractEnabled;

				EventManager.Instance.RaiseEvent(new ToggleAttractEvent(attractEnabled, attractScript));
			}
		}
	}
}