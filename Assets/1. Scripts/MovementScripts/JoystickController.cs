using JoystickData;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.UnityExtensions;

namespace MovementScripts
{
	[RequireComponent(typeof(JoystickNumber), typeof(CharacterController))]
	public class JoystickController : BetterMonoBehaviour
	{
		[SerializeField] private float speed = 10.0f;

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
			if (boundsRestrictor.IsOutOfBounds(CachedTransform.position,
				MovementHandler.GetClampedJoystickAxes(joystickNumber, 1)))
			{
				Vector3 directionToCamera = (mainCamera.transform.position - CachedTransform.position).normalized;

				characterController.Move(speed * directionToCamera);
				return;
			}

			characterController.Move(speed * Time.deltaTime * MovementHandler.GetClampedJoystickAxes(joystickNumber, 1));
		}
	}
}