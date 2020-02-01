using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace AI
{
	[RequireComponent(typeof(WanderBehaviour), typeof(AttractBehaviour))]
	public class SheepBehaviourManager : BetterMonoBehaviour
	{
		public float MaxAcceleration = 13.0f;
		public float MaxVelocity = 5.0f;
		public float RotateSpeed = 13;
 
		public float WanderRate = 0.4f;

		public float WanderOffset = 1.3f; 
		public float WanderRadius = 2.0f;
		
		private WanderBehaviour wanderBehaviour;
		private AttractBehaviour attractBehaviour;
		
		private void Awake()
		{
			wanderBehaviour = GetComponent<WanderBehaviour>();
			attractBehaviour = GetComponent<AttractBehaviour>();
		}

		private void ActivateWander()
		{
			attractBehaviour.enabled = false;
			wanderBehaviour.enabled = true;
		}

		private void ActivateAttract()
		{
			wanderBehaviour.enabled = false;
			attractBehaviour.enabled = true;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				ActivateAttract();
			}
		}
		
		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				ActivateWander();
			}
		}
	}
}