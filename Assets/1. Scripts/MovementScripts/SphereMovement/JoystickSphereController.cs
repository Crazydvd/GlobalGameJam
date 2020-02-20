using JoystickData;
using UnityEngine;
using VDFramework;

namespace MovementScripts.SphereMovement
{
	[RequireComponent(typeof(JoystickNumber), typeof(Rigidbody))]
	public class JoystickSphereController : BetterMonoBehaviour
	{
		[SerializeField] private float speed = 5.0f;
		
		private uint joystickNumber;
		private Rigidbody rigidBody;

		private void Awake()
		{
			joystickNumber = GetComponent<JoystickNumber>();
			rigidBody = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			Vector3 acceleration = CachedTransform.TransformDirection(JoystickInput.GetAxes(joystickNumber));
			Vector3 moveDirection = rigidBody.position + Time.deltaTime * speed * acceleration;
			
			rigidBody.MovePosition(moveDirection);
		}
	}
}