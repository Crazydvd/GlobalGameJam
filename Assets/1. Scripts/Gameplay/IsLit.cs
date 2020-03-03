using VDFramework;

public class IsLit : BetterMonoBehaviour
{
	public bool IsInLight()
	{
		if (!LightRayCaster.IsInitialized)
		{
			return false;
		}

		return LightRayCaster.Instance.IsLit(gameObject);
	}
}
