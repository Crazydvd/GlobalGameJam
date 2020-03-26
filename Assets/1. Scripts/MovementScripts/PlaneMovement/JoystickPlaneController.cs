using JoystickData;
using UnityEngine;
using VDFramework;
using VDFramework.UnityExtensions;

namespace MovementScripts.PlaneMovement
{
	[RequireComponent(typeof(JoystickNumber), typeof(CharacterController))]
	public class JoystickPlaneController : BetterMonoBehaviour
	{
		[SerializeField]
		private float speed = 10.0f;

		private CharacterController characterController;
		private ScreenBoundsRestrictor boundsRestrictor;

		private Camera mainCamera;
		private uint joystickNumber;

		private void Awake()
		{
			mainCamera = Camera.main;

			joystickNumber = gameObject.GetComponent<JoystickNumber>();
			characterController = gameObject.GetComponent<CharacterController>();
			boundsRestrictor = gameObject.EnsureComponent<ScreenBoundsRestrictor>();
		}

		private void Update()
		{
			Vector3 movement = JoystickInput.GetClampedAxes(joystickNumber, 1);

			if (boundsRestrictor.IsOutOfBounds(CachedTransform.position, movement))
			{
				MoveTowardsCamera();
				return;
			}

			characterController.SimpleMove(speed * movement);
		}

		private void MoveTowardsCamera()
		{
			Vector3 directionToCamera = (mainCamera.transform.position - CachedTransform.position).normalized;

			characterController.SimpleMove(speed * directionToCamera);
		}
	}
}