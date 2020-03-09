using UnityEngine;
using VDFramework;

namespace MovementScripts.SphereMovement
{
	public class CameraSphereOffset : BetterMonoBehaviour
	{
		[SerializeField]
		private string tagToSearchFor = "Player";

		[SerializeField, Header("Will search for the tag if empty.")]
		private GameObject target;

		[SerializeField]
		private float offset;

		private void Start()
		{
			if (!target)
			{
				target = GameObject.FindGameObjectWithTag(tagToSearchFor);
			}
		}

		private void LateUpdate()
		{
			FindSpherePosition();
		}

		private void FindSpherePosition()
		{
			if (!target)
			{
				return;
			}

			Vector3 targetPosition = target.transform.position;
			CachedTransform.position = targetPosition;

			CachedTransform.rotation = Quaternion.LookRotation(-targetPosition,
				target.transform.forward);

			CachedTransform.Translate(CachedTransform.InverseTransformDirection(-CachedTransform.forward) * offset);
		}
	}
}