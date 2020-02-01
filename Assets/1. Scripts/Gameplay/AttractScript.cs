//#define DRAW_COLLIDER_LINES

using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace Gameplay
{
	[RequireComponent(typeof(SphereCollider))]
	[ExecuteInEditMode]
	public class AttractScript : BetterMonoBehaviour
	{
		public Vector3 LureOrigin => CachedTransform.position + sphereCollider.center;

		[SerializeField] private float attractRadius = 5.0f;

		private SphereCollider sphereCollider;

		private void Awake()
		{
			sphereCollider = gameObject.GetComponent<SphereCollider>();
			sphereCollider.radius = attractRadius;
			sphereCollider.isTrigger = true;
		}

#if UNITY_EDITOR && DRAW_COLLIDER_LINES
		private void Update()
		{
			Debug.DrawLine(CachedTransform.position + CachedTransform.forward * attractRadius,
				CachedTransform.position + CachedTransform.right * attractRadius);
			Debug.DrawLine(CachedTransform.position + CachedTransform.right * attractRadius,
				CachedTransform.position + -CachedTransform.forward * attractRadius);
			Debug.DrawLine(CachedTransform.position + -CachedTransform.forward * attractRadius,
				CachedTransform.position + -CachedTransform.right * attractRadius);
			Debug.DrawLine(CachedTransform.position + -CachedTransform.right * attractRadius,
				CachedTransform.position + CachedTransform.forward * attractRadius);
		}
#endif

		private void OnValidate()
		{
			GetComponent<SphereCollider>().radius = attractRadius;
		}
	}
}