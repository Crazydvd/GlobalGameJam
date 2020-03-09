using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using VDFramework;

namespace MovementScripts.PlaneMovement
{
	[RequireComponent(typeof(Camera))]
	public class ZoomCameraToFitAll : BetterMonoBehaviour
	{
		[SerializeField]
		private Vector2 minMaxDistance = new Vector2(15.0f, 30.0f);

		[SerializeField]
		private string tagToSearchFor = "Player";

		[SerializeField, Header("Will search for the tag if empty.")]
		private GameObject[] objects = new GameObject[0];

		private ICameraMover cameraMover;

		private float aspectRatio;
		private float tanFOV;

		private Camera cameraComponent;

		private void Awake()
		{
			cameraComponent = GetComponent<Camera>();
			cameraMover = GetComponent<ICameraMover>();

			tanFOV = Mathf.Tan(Mathf.Deg2Rad * cameraComponent.fieldOfView / 2.0f);

			aspectRatio = Screen.height / (float) Screen.width;
		}

		private void Start()
		{
			if (objects.Length == 0)
			{
				objects = GameObject.FindGameObjectsWithTag(tagToSearchFor);
			}
		}

		private void LateUpdate()
		{
			Vector3[] maxandMinPositions = FindFurthestObjects();

			ZoomCamera(maxandMinPositions);
		}

		private Vector3[] FindFurthestObjects()
		{
			Vector3 highestZPosition = new Vector3(0, 0, float.NegativeInfinity);
			Vector3 lowestZPosition = new Vector3(0, 0, float.PositiveInfinity);
			;

			foreach (GameObject @object in objects)
			{
				Vector3 position = @object.transform.position;

				if (position.z > highestZPosition.z)
				{
					highestZPosition = position;
				}

				if (position.z < lowestZPosition.z)
				{
					lowestZPosition = position;
				}
			}

			return new[] {highestZPosition, lowestZPosition};
		}

		private void ZoomCamera(IReadOnlyList<Vector3> maxMinPos)
		{
			float distanceBetweenPlayers = (maxMinPos[0] - maxMinPos[1]).magnitude;

			float cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFOV;

			Vector3 middle = CachedTransform.position;
			middle.y = 0;

			Vector3 offset = cameraMover.Offset;
			float y = (middle + -CachedTransform.forward * cameraDistance).y;

			offset.y = Mathf.Clamp(y, minMaxDistance.x, minMaxDistance.y);
			cameraMover.Offset = offset;
		}
	}
}