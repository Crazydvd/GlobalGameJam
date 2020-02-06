using VDFramework.EventSystem;
using Gameplay;

public class ToggleAttractEvent : VDEvent
{
	public readonly bool ToggleOn;
	public readonly AttractScript AttractScript;

	public ToggleAttractEvent(bool isEnabled, AttractScript attractScript)
	{
		ToggleOn = isEnabled;
		AttractScript = attractScript;
	}
}