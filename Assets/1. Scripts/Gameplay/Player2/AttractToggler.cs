using Enums;
using JoystickData;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.EventSystem;

namespace Gameplay
{
	public class AttractToggler : BetterMonoBehaviour
	{
		private uint joystickNumber;

		private bool attractEnabled;

		private AttractScript attractScript;
				
		private void Awake()
		{
			attractScript = GetComponent<AttractScript>();
			joystickNumber = GetComponent<JoystickNumber>();
		}

		private void Update()
		{
			if (JoystickInput.GetButtonDown(joystickNumber, JoystickButton.RightShoulderButton))
			{
				attractEnabled = !attractEnabled;

				EventManager.Instance.RaiseEvent(new ToggleAttractEvent(attractEnabled, attractScript));
			}
		}
	}
}