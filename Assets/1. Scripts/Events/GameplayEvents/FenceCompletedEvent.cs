using Fence;
using VDFramework.EventSystem;

namespace Events.GameplayEvents
{
	public class FenceCompletedEvent : VDEvent
	{
		public readonly FenceCorner Origin;

		public FenceCompletedEvent(FenceCorner origin)
		{
			Origin = origin;
		}
	}
}