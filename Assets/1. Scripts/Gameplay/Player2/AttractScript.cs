using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace Gameplay
{
	public class AttractScript : BetterMonoBehaviour
	{
		public Vector3 LureOrigin => sheepatTractor.transform.position;

		public float AttractRadius => attractRadius;

		[SerializeField] private GameObject sheepatTractor;

		private ForceField forceFieldRadius;

		[SerializeField] private float attractRadius = 5.0f;

		private void Awake()
		{
			GetComponentInChildren<ForceField>().radius = attractRadius * 2;
		}

		private void Update()
		{
			Debug.DrawLine(LureOrigin + Vector3.forward * attractRadius, LureOrigin + Vector3.right * attractRadius);
			Debug.DrawLine(LureOrigin + Vector3.right * attractRadius, LureOrigin + Vector3.back * attractRadius);
			Debug.DrawLine(LureOrigin + Vector3.back * attractRadius, LureOrigin + Vector3.left * attractRadius);
			Debug.DrawLine(LureOrigin + Vector3.left * attractRadius, LureOrigin + Vector3.forward * attractRadius);
		}

		private void OnValidate()
		{
			GetComponentInChildren<ForceField>().radius = attractRadius * 2;
		}
	}
}