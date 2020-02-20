using UnityEngine;
using VDFramework;

namespace Utility
{
	public class RotateWithSlope : BetterMonoBehaviour
	{
		private void LateUpdate()
		{
			if (Physics.Raycast(new Ray(CachedTransform.position, -CachedTransform.up), out RaycastHit hitInfo))
			{
				CachedTransform.rotation *= Quaternion.FromToRotation(CachedTransform.up, hitInfo.normal);
			}
		}
	}
}