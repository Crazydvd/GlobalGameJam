using JoystickData;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.UnityExtensions;

namespace MovementScripts
{
	[RequireComponent(typeof(JoystickNumber))]
	public class JoystickController : BetterMonoBehaviour
	{
		[SerializeField] private float speed = 10.0f;
		
		private CharacterController characterController;
		
		private uint joystickNumber;

		private void Awake()
		{
			joystickNumber = gameObject.GetComponent<JoystickNumber>();
			characterController = gameObject.EnsureComponent<CharacterController>();
		}

		private void Update()
		{
			characterController.SimpleMove(speed * MovementHandler.GetClampedJoystickInput(joystickNumber, 1));
		}
	}
}