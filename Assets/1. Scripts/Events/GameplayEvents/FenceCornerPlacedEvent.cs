using Fence;
using VDFramework.EventSystem;

namespace Events.GameplayEvents
{
	public class FenceCornerPlacedEvent : VDEvent
	{
		public readonly FenceCorner Corner;

		public FenceCornerPlacedEvent(FenceCorner corner)
		{
			Corner = corner;
		}
	}
}