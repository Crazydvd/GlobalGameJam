using VDFramework;

namespace Gameplay
{
	public class LightChecker : BetterMonoBehaviour
	{
		public bool IsInLight()
		{
			return LightRayCaster.Instance.IsLit(gameObject);
		}
	}
}