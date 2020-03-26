// ReSharper disable UnassignedField.Global

using System;
using UnityEngine;

namespace Structs
{
	[Serializable]
	public struct SheepMovementSettings
	{
		public float MaxAcceleration; //15
		public float MaxVelocity; //3.3
		public float RotateSpeed; //13

		[Space]
		public float WanderOffset; //4

		public float WanderRate; //0.4
		public float WanderRadius; //2
	}
}