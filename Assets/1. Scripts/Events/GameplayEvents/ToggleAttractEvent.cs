using Gameplay.Player2;
using VDFramework.EventSystem;

namespace Events.GameplayEvents
{
	public class ToggleAttractEvent : VDEvent
	{
		public readonly bool ToggleOn;
		public readonly AttractScript AttractScript;
		public readonly int Priority;

		public ToggleAttractEvent(bool isEnabled, AttractScript attractScript, int priority)
		{
			ToggleOn = isEnabled;
			AttractScript = attractScript;
			Priority = priority;
		}
	}
}