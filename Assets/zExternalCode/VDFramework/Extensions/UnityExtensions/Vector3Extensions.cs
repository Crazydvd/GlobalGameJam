using UnityEngine;

namespace VDUnityFramework.UnityExtensions
{
	public static class Vector3Extensions
	{
		public static Vector3 Round(this Vector3 vec3)
		{
			return new Vector3(Mathf.Round(vec3.x), Mathf.Round(vec3.y), Mathf.Round(vec3.z));
		}
	}
}