using UnityEngine;
using UnityEngine.Serialization;
using VDFramework;

namespace Gameplay.Player2
{
	public class AttractScript : BetterMonoBehaviour
	{
		public Vector3 LureOrigin => sheepAttractor.transform.position;

		public float AttractRadius => attractRadius;

		[FormerlySerializedAs("sheepatTractor"), SerializeField]
		private GameObject sheepAttractor;

		private ForceField forceFieldRadius;

		[SerializeField]
		private float attractRadius = 5.0f;

		private void Awake()
		{
			GetComponentInChildren<ForceField>().Radius = attractRadius * 2;
		}

		private void Update()
		{
			DrawLine(Vector3.forward, Vector3.right);
			DrawLine(Vector3.right, Vector3.back);
			DrawLine(Vector3.back, Vector3.left);
			DrawLine(Vector3.left, Vector3.forward);
		}

		private void OnValidate()
		{
			GetComponentInChildren<ForceField>().Radius = attractRadius * 2;
		}

		private void DrawLine(Vector3 localStart, Vector3 localEnd)
		{
			Debug.DrawLine(LureOrigin + localStart * attractRadius, LureOrigin + localEnd * attractRadius);
		}
	}
}