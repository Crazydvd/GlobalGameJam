using JoystickData;
using VDUnityFramework.BaseClasses;

namespace Gameplay
{
	public class AttractToggler : BetterMonoBehaviour
	{
		private AttractScript attractScript;
		
		private void Awake()
		{
			attractScript = GetComponent<AttractScript>();
		}

		private void Update()
		{
			if (JoystickButtonHandler.IsShoulderButtonPressed(GetComponent<JoystickNumber>()))
			{
				attractScript.enabled ^= true;
			}
		}
	}
}