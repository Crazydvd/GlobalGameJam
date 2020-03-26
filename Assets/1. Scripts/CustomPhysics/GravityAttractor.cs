using UnityEngine;
using VDFramework;

namespace CustomPhysics
{
	public class GravityAttractor : BetterMonoBehaviour
	{
		[SerializeField]
		private float gravity = -9.807f;

		public void Attract(Rigidbody body)
		{
			// direction from center to body
			Vector3 targetDir = (body.position - CachedTransform.position).normalized;

			//the body's current local up direction
			Vector3 bodyUp = body.transform.up;

			body.AddForce(targetDir * gravity);
			body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
		}
	}
}