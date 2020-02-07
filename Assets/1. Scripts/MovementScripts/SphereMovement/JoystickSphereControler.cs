using System;
using Enums;
using JoystickData;
using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace MovementScripts.SphereMovement
{
	[RequireComponent(typeof(JoystickNumber), typeof(Rigidbody))]
	public class JoystickSphereController : BetterMonoBehaviour
	{
		private uint joystickNumber;

		private Rigidbody rigidBody;

		private void Awake()
		{
			joystickNumber = GetComponent<JoystickNumber>();
			rigidBody = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			if (JoystickInput.GetButtonDown(joystickNumber, JoystickButton.RightShoulderButton))
			{
				
			}
		}
	}
}