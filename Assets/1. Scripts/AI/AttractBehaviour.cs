using System;
using Gameplay;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using Random = UnityEngine.Random;

namespace AI
{
	public class AttractBehaviour : BetterMonoBehaviour
	{
		private AttractScript attractScript;

		private SheepBehaviourManager sheepBehaviourManager;

		private float wanderOrientation = 0;

		private void Awake()
		{
			sheepBehaviourManager = GetComponent<SheepBehaviourManager>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				attractScript = other.GetComponent<AttractScript>();
			}
		}

		private void FixedUpdate()
		{
			if (!attractScript)
			{
				return;
			}
			
			Vector3 accelleration = GetSteerOrientation();

			Steer(accelleration);
			LookWhereYouGoing();
		}
		
		private void LookWhereYouGoing()
		{
			Vector3 direction = CachedRigidBody.velocity;
        
			direction.Normalize();

			if (direction.magnitude > 0.001)
			{
				float targetRotation = -1 * (Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg);
				float newRotation = Mathf.LerpAngle(CachedRigidBody.rotation.eulerAngles.y, targetRotation, Time.deltaTime * sheepBehaviourManager.RotateSpeed);
				CachedRigidBody.rotation = Quaternion.Euler(0,newRotation,0);
            
			}
        
		}
		
		private void Steer(Vector3 linearAcceleration)
		{
			CachedRigidBody.velocity += linearAcceleration * Time.deltaTime;

			if (CachedRigidBody.velocity.magnitude > sheepBehaviourManager.MaxVelocity)
			{
				CachedRigidBody.velocity = CachedRigidBody.velocity.normalized * sheepBehaviourManager.MaxVelocity;
			}
		}
		
		private Vector3 GetSteerOrientation()
		{
			float sheepOrientation = OrientationInRadians(); //rotation of the sheep in radians

			wanderOrientation += GetRandomNumber() * sheepBehaviourManager.WanderRate;

			float targetOrientation = wanderOrientation + sheepOrientation; //get new orientation to rotate to 

			Vector3 targetPosition = attractScript.LureOrigin;

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
			return CachedRigidBody.rotation.eulerAngles.y * Mathf.Deg2Rad;
		}

		private float GetRandomNumber()
		{
			return Random.value - Random.value;
		}
	}
}