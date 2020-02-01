using UnityEngine;

namespace VDUnityFramework.BaseClasses
{
	public class BetterMonoBehaviour : MonoBehaviour
	{
		private Transform cachedTransform;

		public Transform CachedTransform
		{
			get
			{
				if (cachedTransform == null)
				{
					cachedTransform = base.transform;
				}

				return cachedTransform;
			}
		}

		private Rigidbody cachedRigidBody;

		public Rigidbody CachedRigidBody
		{
			get
			{
				if (cachedRigidBody == null)
				{
					cachedRigidBody = GetComponent<Rigidbody>();
				}

				return cachedRigidBody;
			}
		}
	}
}