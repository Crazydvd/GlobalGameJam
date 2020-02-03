using Enums;
using JoystickData;
using VDUnityFramework.BaseClasses;

namespace Gameplay
{
	public class AttractToggler : BetterMonoBehaviour
	{
		private uint joystickNumber;
		
		private AttractScript attractScript;
		
		private void Awake()
		{
			joystickNumber = GetComponent<JoystickNumber>();
			attractScript = GetComponent<AttractScript>();
		}

		private void Update()
		{
			if (JoystickButtonHandler.Instance.GetButtonDown(joystickNumber, JoystickButton.RightShoulderButton))
			{
				attractScript.enabled ^= true;
			}
		}
	}
}