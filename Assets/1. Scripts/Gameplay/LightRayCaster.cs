using UnityEngine;
using VDFramework.Singleton;

public class LightRayCaster : Singleton<LightRayCaster>
{
	public bool IsLit(GameObject objectToCheck)
	{
		Ray ray = new Ray(CachedTransform.position, objectToCheck.transform.position - CachedTransform.position);
		Debug.DrawLine(CachedTransform.position, objectToCheck.transform.position);

		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			return hit.collider.gameObject == objectToCheck;
		}

		//Debug.Break();

		return true;
	}
}