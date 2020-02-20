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

		private void OnToggleAttract(ToggleAttractEvent toggleAttractEvent)
		{
			sphereMesh.enabled = toggleAttractEvent.ToggleOn;
		}
	}
}
