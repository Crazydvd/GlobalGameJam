using UnityEngine;
using VDFramework;

namespace Gameplay.Player2
{
	public class AttractScript : BetterMonoBehaviour
	{
		public Vector3 LureOrigin => sheepAttractor.transform.position;

		[SerializeField]
		private float attractRadius = 5.0f;

		public float AttractRadius => attractRadius;

		[SerializeField]
		private GameObject sheepAttractor;

		private ForceField forceFieldRadius;

		private void Awake()
		{
			GetComponentInChildren<ForceField>().Radius = attractRadius * 2;
		}
	}
}