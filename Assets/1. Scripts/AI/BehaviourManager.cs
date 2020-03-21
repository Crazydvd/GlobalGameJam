using Gameplay;
using Gameplay.Player2;
using UnityEngine;
using AI;
using VDFramework;
using VDFramework.EventSystem;

[RequireComponent(typeof(AttractBehaviour))]
public class BehaviourManager: MonoBehaviour
{
	public float MaxAcceleration = 13.0f;
	public float MaxVelocity = 3.0f;
	public float RotateSpeed = 5f;

	public float WanderRate = 0.4f;

	public float WanderOffset = 4f;
	public float WanderRadius = 2f;

	private WonderAI wonderBehaviour;
	private AttractBehaviour attractBehaviour;

	private AttractScript attractScript;
	[HideInInspector]
	public Vector3 LureOrigin => attractScript.LureOrigin;

	private void Awake()
	{
		wonderBehaviour = GetComponent<WonderAI>();
		attractBehaviour = GetComponent<AttractBehaviour>();

		EventManager.Instance.AddListener<ToggleAttractEvent>(OnToggleAttract);
	}

	private void OnDestroy()
	{
		if (EventManager.IsInitialized)
		{
			EventManager.Instance.RemoveListener<ToggleAttractEvent>(OnToggleAttract);
		}
	}

	private void Update()
	{
		if (wonderBehaviour.enabled == false)
		{
			Debug.Log(wonderBehaviour);
			ActivateWonder();
		}
	
	}

	private void ActivateWonder()
	{
		attractBehaviour.enabled = false;
		wonderBehaviour.enabled = true;
	}

	private void ActivateAttract()
	{
		wonderBehaviour.enabled = false;
		attractBehaviour.enabled = true;
	}

	private void OnToggleAttract(ToggleAttractEvent toggleAttractEvent)
	{
		if (!attractScript)
		{
			attractScript = toggleAttractEvent.AttractScript;
		}

		//TODO: store AttractScript or something and continueously check if we're in range
		if (toggleAttractEvent.ToggleOn && IsWithinAttractRange(attractScript))
		{
			ActivateAttract();
		}
		else
		{
			ActivateWonder();
		}
	}

	private bool IsWithinAttractRange(AttractScript attractScript)
	{
		return Vector3.Distance(transform.position, attractScript.LureOrigin) <= attractScript.AttractRadius;
	}
}

