using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace CustomPhysics
{
	[RequireComponent(typeof(CharacterController))]
	public class PlanetGravity : BetterMonoBehaviour
	{
		[SerializeField] private float gravity = -9.81f;

		// CharacterController because our movement is unrealistic.
		private CharacterController characterController;
		private Vector3 directionFromCenter;

		private Planet planet;

		private void Awake()
		{
			planet = FindObjectOfType<Planet>();
			characterController = GetComponent<CharacterController>();

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
				
			characterController.Move(Time.deltaTime * gravity * directionFromCenter);
			
			Debug.DrawLine(CachedTransform.position, planet.CachedTransform.position);
		}

		private static float SquaredDeltaTime()
		{
			return Time.deltaTime * Time.deltaTime;
		}
	}
}