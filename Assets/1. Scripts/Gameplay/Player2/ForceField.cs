using Events.GameplayEvents;
using UnityEngine;
using VDFramework.EventSystem;

namespace Gameplay.Player2
{
	public class ForceField : MonoBehaviour
	{
		private MeshRenderer sphereMesh;
		
		public float Radius
		{
			get => transform.localScale.x;
			set => transform.localScale = new Vector3(value, value, value);
		}

		private void Awake()
		{
			sphereMesh = GetComponent<MeshRenderer>();
		}

		private void OnEnable()
		{
			AddListeners();
		}

		private void OnDisable()
		{
			RemoveListeners();
		}

		private void AddListeners()
		{
			EventManager.Instance.AddListener<ToggleAttractEvent>(OnToggleAttract);
		}

		private void RemoveListeners()
		{
			if (EventManager.IsInitialized)
			{
				EventManager.Instance.RemoveListener<ToggleAttractEvent>(OnToggleAttract);
			}
		}

		private void OnToggleAttract(ToggleAttractEvent toggleAttractEvent)
		{
			sphereMesh.enabled = toggleAttractEvent.ToggleOn;
		}
	}
}
