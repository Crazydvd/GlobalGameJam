using VDFramework.EventSystem;

namespace Events.GameplayEvents
{
	public class FenceCompletedEvent : VDEvent
	{
		public readonly uint SheepCollected;
		
		public FenceCompletedEvent(uint sheepCollected)
		{
			SheepCollected = sheepCollected;
		}
	}
}