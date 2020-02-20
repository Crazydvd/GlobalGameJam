using VDFramework;

namespace JoystickData
{
	public class JoystickNumber : BetterMonoBehaviour
	{
		public uint Number = 1;

		public static implicit operator uint(JoystickNumber joystickNumber)
		{
			return joystickNumber.Number;
		}
	}
}