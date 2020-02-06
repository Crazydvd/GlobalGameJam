using Gameplay;
using System;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.EventSystem;

namespace AI
{
	[RequireComponent(typeof(WanderBehaviour), typeof(AttractBehaviour))]
	public class SheepBehaviourManager : BetterMonoBehaviour
	{
		public float MaxAcceleration = 13.0f;
		public float MaxVelocity = 3.0f;
		public float RotateSpeed = 10;
 
		public float WanderRate = 0.4f;

		public float WanderOffset = 4f; 
		public float WanderRadius = 2f;
		
		private WanderBehaviour wanderBehaviour;
		private AttractBehaviour attractBehaviour;

		private AttractScript attractScript;

		public Vector3 LureOrigin => attractScript.LureOrigin;
		
		private void Awake()
		{
			wanderBehaviour = GetComponent<WanderBehaviour>();
			attractBehaviour = GetComponent<AttractBehaviour>();

			EventManager.Instance.AddListener<ToggleAttractEvent>(OnToggleAttract);
		}

		private void OnDestroy()
		{
			if (EventManager.IsInitialized)
			{
				EventManager.Instance.RemoveListener<ToggleAttractEvent>(OnToggleAttract);
			}
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

		private void OnToggleAttract(ToggleAttractEvent toggleAttractEvent)
		{
			if (!attractScript)
			{
				attractScript = toggleAttractEvent.AttractScript;
			}

			//TODO: store AttractScript or something and continueously check if we're in range
			if (toggleAttractEvent.ToggleOn && IsWithinAttractRange(attractScript))
			{
				ActivateAttract();
			}
			else
			{
				ActivateWander();
			}
		}

		private bool IsWithinAttractRange(AttractScript attractScript)
		{
			return Vector3.Distance(CachedTransform.position, attractScript.LureOrigin) <= attractScript.AttractRadius;
		}
	}
}