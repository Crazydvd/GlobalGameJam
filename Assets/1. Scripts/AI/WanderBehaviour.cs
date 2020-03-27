using System;
using System.Collections;
using UnityEngine;
using VDFramework;
using VDFramework.Utility;
using Random = UnityEngine.Random;

namespace AI
{
	public class WanderBehaviour : BetterMonoBehaviour
	{
		private enum WanderState
		{
			RotateLeft,
			RotateRight,
			Walking,
			Idle,
		}

		//private BehaviourManager BehaviourManager;
		public float moveSpeed = 3f;
		public float rotSpeed = 100f;

		private WanderState currentState = WanderState.Idle;

		private Coroutine coroutine = null;

		// Update is called once per frame
		private void Update()
		{
			if (coroutine == null)
			{
				coroutine = StartCoroutine(Wander());
			}
			
			switch (currentState)
			{
				case WanderState.RotateLeft:
				case WanderState.RotateRight:
					Rotate(currentState);
					break;
				case WanderState.Walking:
					Walk();
					break;
				case WanderState.Idle:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private IEnumerator Wander()
		{
			int rotateTime = Random.Range(1, 3);
			int rotateWait = Random.Range(1, 3);
			int walkWait = Random.Range(1, 3);
			int walkTime = Random.Range(1, 8);

			currentState = WanderState.Idle;
			
			yield return new WaitForSeconds(walkWait);

			currentState = WanderState.Walking;

			yield return new WaitForSeconds(walkTime);

			currentState = WanderState.Idle;
			
			yield return new WaitForSeconds(rotateWait);

			currentState = RandomUtil.GetRandom(WanderState.RotateLeft, WanderState.RotateRight);

			yield return new WaitForSeconds(rotateTime);

			StopCoroutine(coroutine);
			coroutine = null;
		}
		
		private void Walk()
		{
			transform.position += Time.deltaTime * moveSpeed * transform.forward;
		}
		
		private void Rotate(WanderState state)
		{
			float modifier = state == WanderState.RotateRight ? 1 : -1;

			float rotationSpeed = rotSpeed * modifier;

			CachedTransform.RotateAround(CachedTransform.position, CachedTransform.up, rotationSpeed * Time.deltaTime);
		}
	}
}