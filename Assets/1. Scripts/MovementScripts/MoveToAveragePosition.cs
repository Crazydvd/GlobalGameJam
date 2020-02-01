using System;
using System.Linq;
using Interfaces;
using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace MovementScripts
{
	public class MoveToAveragePosition : BetterMonoBehaviour, ICameraMover
	{
		[SerializeField] private string tagToSearchFor = "Player";

		[Header("Will search for the tag if empty.")]
		[SerializeField] private GameObject[] objects = new GameObject[0];

		
		[SerializeField] private Vector3 offset;
		// ReSharper disable once ConvertToAutoProperty
		public Vector3 Offset
		{
			get => offset;
			set => offset = value;
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
			CachedTransform.position = FindAveragePosition() + offset;
		}

		private Vector3 FindAveragePosition()
		{
			return objects.Aggregate(Vector3.zero, (current, gameobject) => current + gameobject.transform.position) /
				   objects.Length;
		}
	}
}