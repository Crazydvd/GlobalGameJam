using UnityEngine;

namespace CustomPhysics
{
	public class FouxGravity : MonoBehaviour
	{
		public GravityAttractor attractor;

		private Rigidbody rigidBody;

		// Start is called before the first frame update
		void Start()
		{
			rigidBody = GetComponent<Rigidbody>();

			rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
			rigidBody.GetComponent<Rigidbody>().useGravity = false;
		}

		// Update is called once per frame
		void FixedUpdate()
		{
			attractor.Attract(transform);
		}
	}
}