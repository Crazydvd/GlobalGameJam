using Gameplay;
using UnityEngine;
using VDFramework;
using VDFramework.UnityExtensions;

namespace CustomPhysics
{
	public class FouxGravity : BetterMonoBehaviour
	{
		private Rigidbody rigidBody;

		private GravityAttractor[] gravityAttractors;

		// Start is called before the first frame update
		void Start()
		{
			gravityAttractors = FindObjectsOfType<GravityAttractor>();
			
			rigidBody = gameObject.EnsureComponent<Rigidbody>();
			rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
			rigidBody.useGravity = false;
		}

		private GravityAttractor FindClosestAttractor()
		{
			if (gravityAttractors.Length == 1)
			{
				return gravityAttractors[0];
			}
			
			GravityAttractor closest = null;
			float closestDistance = float.PositiveInfinity;
			
			foreach (GravityAttractor gravityAttractor in gravityAttractors)
			{
				float distance = Vector3.Distance(CachedTransform.position, gravityAttractor.CachedTransform.position);
				if ((distance >= closestDistance))
				{
					continue;
				}

				closestDistance = distance;
				closest = gravityAttractor;
			}

			return closest;
		}

		// Update is called once per frame
		void FixedUpdate()
		{
			FindClosestAttractor().Attract(rigidBody);
		}
	}
}