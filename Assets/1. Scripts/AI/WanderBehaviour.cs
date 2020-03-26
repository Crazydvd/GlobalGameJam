using System.Collections;
using UnityEngine;
using VDFramework;

namespace AI
{
	public class WanderBehaviour : BetterMonoBehaviour
	{
		//private BehaviourManager BehaviourManager;
		public float moveSpeed = 3f;
		public float rotSpeed = 100f;

		private bool isWondering = false;
		private bool isRotatingLeft = false;
		private bool isRotatingRight = false;
		private bool isWalking = false;

		// Update is called once per frame
		void Update()
		{
			if (isWondering == false)
			{
				StartCoroutine(Wonder());
			}

			if (isRotatingRight == true)
			{
				transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
			}

			if (isRotatingLeft == true)
			{
				transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
			}

			if (isWalking == true)
			{
				transform.position += transform.forward * moveSpeed * Time.deltaTime;
			}
		}

		IEnumerator Wonder()
		{
			int rotTime = Random.Range(1, 3);
			int rotateWait = Random.Range(1, 3);
			int rotateLorR = Random.Range(0, 3);
			int walkWait = Random.Range(1, 3);
			int walkTime = Random.Range(1, 8);

			isWondering = true;

			yield return new WaitForSeconds(walkWait);
			isWalking = true;
			yield return new WaitForSeconds(walkTime);
			isWalking = false;
			yield return new WaitForSeconds(rotateWait);
			if (rotateLorR == 1)
			{
				isRotatingRight = true;
				yield return new WaitForSeconds(rotTime);
				isRotatingRight = false;
			}

			if (rotateLorR == 2)
			{
				isRotatingLeft = true;
				yield return new WaitForSeconds(rotTime);
				isRotatingLeft = false;
			}

			isWondering = false;
		}
	}
}