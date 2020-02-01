using Singleton;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.UnityExtensions;

namespace InputScripts
{
	public class JoystickController : BetterMonoBehaviour
	{
		[SerializeField] private uint joystickNumber = 1;
		
		[SerializeField] private float speed = 10.0f;
		
		private CharacterController characterController;

		private void Awake()
		{
			characterController = gameObject.EnsureComponent<CharacterController>();
		}

		private void Update()
		{
			characterController.SimpleMove(speed * MovementHandler.GetClampedJoystickInput(joystickNumber, 1));
		}
	}
}