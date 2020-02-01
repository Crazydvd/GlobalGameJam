using System;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.UnityExtensions;

namespace MovementScripts
{
	public class ZoomCameraToFitAll : BetterMonoBehaviour
	{
		[SerializeField] private Vector3 offset = Vector3.zero;
		[SerializeField] private string tagToSearchFor = "Player";

		[Header("Will search for the tag if empty.")] [SerializeField]
		private GameObject[] objects = new GameObject[0];

		private float aspectRatio;
		private float tanFOV;

		private Camera mainCamera;

		private void Awake()
		{
			mainCamera = Camera.main;

			aspectRatio = Screen.height / (float) Screen.width;
			tanFOV = Mathf.Tan(Mathf.Deg2Rad * mainCamera.fieldOfView / 2.0f);
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
			GameObject[] objects = FindFurthestObjects();
		}

		private GameObject[] FindFurthestObjects()
		{
			throw new NotImplementedException();
		}
	}
}