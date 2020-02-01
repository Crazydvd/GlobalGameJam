using System;
using System.Linq;
using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace MovementScripts
{
	public class MoveToAveragePosition : BetterMonoBehaviour
	{
		[SerializeField] private Vector3 offset = Vector3.zero;
		[SerializeField] private string tagToSearchFor = "Player";

		[Header("Will search for the tag if empty.")]
		[SerializeField] private GameObject[] objects = new GameObject[0];

		private void Start()
		{
			if (objects.Length == 0)
			{
				objects = GameObject.FindGameObjectsWithTag(tagToSearchFor);
			}
		}

		private void LateUpdate()
		{
			CachedTransform.position = FindAveragePosition() + offset;
		}

		private Vector3 FindAveragePosition()
		{
			return objects.Aggregate(Vector3.zero, (current, gameobject) => current + gameobject.transform.position) /
				   objects.Length;
		}
	}
}