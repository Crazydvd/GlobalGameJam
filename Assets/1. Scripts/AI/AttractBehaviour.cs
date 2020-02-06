using System;
using Gameplay;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using Random = UnityEngine.Random;

namespace AI
{
	public class AttractBehaviour : BetterMonoBehaviour
	{
		private SheepBehaviourManager sheepBehaviourManager;

		private float wanderOrientation = 0;

		private new Rigidbody rigidbody;

		private void Awake()
		{
			rigidbody = GetComponent<Rigidbody>();
			sheepBehaviourManager = GetComponent<SheepBehaviourManager>();
		}

		private void FixedUpdate()
		{	
			Vector3 accelleration = GetSteerOrientation();

			Steer(accelleration);
			LookWhereYouGoing();
		}
		
		private void LookWhereYouGoing()
		{
			Vector3 direction = rigidbody.velocity;
        
			direction.Normalize();

			if (direction.magnitude > 0.001)
			{
				float targetRotation = -1 * (Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg);
				float newRotation = Mathf.LerpAngle(rigidbody.rotation.eulerAngles.y, targetRotation, Time.deltaTime * sheepBehaviourManager.RotateSpeed);
				rigidbody.rotation = Quaternion.Euler(0,newRotation,0);
            
			}
        
		}
		
		private void Steer(Vector3 linearAcceleration)
		{
			rigidbody.velocity += linearAcceleration * Time.deltaTime;

			if (rigidbody.velocity.magnitude > sheepBehaviourManager.MaxVelocity)
			{
				rigidbody.velocity = rigidbody.velocity.normalized * sheepBehaviourManager.MaxVelocity;
			}
		}
		
		private Vector3 GetSteerOrientation()
		{
			float sheepOrientation = OrientationInRadians(); //rotation of the sheep in radians

			wanderOrientation += GetRandomNumber() * sheepBehaviourManager.WanderRate;

			float targetOrientation = wanderOrientation + sheepOrientation; //get new orientation to rotate to 

			Vector3 targetPosition = sheepBehaviourManager.LureOrigin;

			targetPosition = targetPosition + (GetOrientationVector(targetOrientation) * sheepBehaviourManager.WanderRadius);

			return Seek(targetPosition);
		}
		
		private Vector3 Seek(Vector3 targetPosition)
		{
			Vector3 acceleration = targetPosition - transform.position;
			acceleration.y = 0;
			acceleration.Normalize();
    
			acceleration *= sheepBehaviourManager.MaxAcceleration;
        
			return acceleration;
		}


		private Vector3 GetOrientationVector(float orientation)
		{
			return new Vector3(Mathf.Cos(-orientation), 0, Mathf.Sin(-orientation));
		}

		private float OrientationInRadians()
		{
			return rigidbody.rotation.eulerAngles.y * Mathf.Deg2Rad;
		}

		private float GetRandomNumber()
		{
			return Random.value - Random.value;
		}
	}
}