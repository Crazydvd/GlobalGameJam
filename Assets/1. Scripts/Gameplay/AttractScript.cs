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

		private void OnValidate()
		{
			GetComponent<SphereCollider>().radius = attractRadius;
		}
	}
}