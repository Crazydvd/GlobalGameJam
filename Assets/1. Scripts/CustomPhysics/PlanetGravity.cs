using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace CustomPhysics
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlanetGravity : BetterMonoBehaviour
	{
		[SerializeField] private float gravity = -9.81f;

		private Rigidbody rigidBody;
		private Vector3 directionFromCenter;

		private Planet planet;

		private void Awake()
		{
			planet = FindObjectOfType<Planet>();
			rigidBody = GetComponent<Rigidbody>();

			if (planet == null)
			{
				throw new UnityException("Could not find object of type planet");
			}
		}

		private void FixedUpdate()
		{
			if (!planet)
			{
				return;
			}

			directionFromCenter = (CachedTransform.position - planet.CachedTransform.position)
				.normalized;
				
			rigidBody.AddForce(Time.deltaTime * gravity * directionFromCenter);
			
			Debug.DrawLine(CachedTransform.position, planet.CachedTransform.position);
		}
	}
}