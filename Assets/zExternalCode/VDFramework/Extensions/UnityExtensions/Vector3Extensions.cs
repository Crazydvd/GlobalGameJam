using UnityEngine;

namespace VDUnityFramework.UnityExtensions
{
	public static class Vector3Extensions
	{
		/// <summary>
		/// returns a new Vector3 where every component is rounded up to the nearest integer.
		/// </summary>
		/// <param name="vec3"></param>
		/// <returns></returns>
		public static Vector3 Ceil(this Vector3 vec3)
		{
			return new Vector3(Mathf.Ceil(vec3.x), Mathf.Ceil(vec3.y), Mathf.Ceil(vec3.z));
		}
	}
}