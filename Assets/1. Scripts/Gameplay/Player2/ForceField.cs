using System;
using UnityEngine;
using VDUnityFramework.EventSystem;

public class ForceField : MonoBehaviour
{
	MeshRenderer sphereMesh;
	public float radius
	{
		get => transform.localScale.x;
		set
		{
			transform.localScale = new Vector3(value, value, value);
		}
	}

	private void Awake()
	{
		EventManager.Instance.AddListener<ToggleAttractEvent>(OnToggleAttract);
		sphereMesh = GetComponent<MeshRenderer>();
	}

	private void OnDestroy()
	{
		if (EventManager.IsInitialized)
		{
			EventManager.Instance.RemoveListener<ToggleAttractEvent>(OnToggleAttract);
		}
	}

	private void OnToggleAttract(ToggleAttractEvent ToggleAE)
	{
		sphereMesh.enabled = ToggleAE.ToggleOn;
	}
}
