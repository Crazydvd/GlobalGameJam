using UnityEngine;
using VDUnityFramework.Singleton;

public class LightRayCaster : Singleton<LightRayCaster>
{
   public bool IsLit(GameObject objectToCheck)
    {
		Ray ray = new Ray(transform.position, objectToCheck.transform.position);
		Debug.DrawLine(transform.position, objectToCheck.transform.position);

		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			return hit.collider.gameObject == objectToCheck;
		}

		Debug.Break();

		return true;
    }
}
