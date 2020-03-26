using System.Collections.Generic;
using System.Linq;
using Events.GameplayEvents;
using Gameplay.Player2;
using Structs;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace AI
{
	[RequireComponent(typeof(WanderBehaviour), typeof(AttractBehaviour))]
	public class SheepBehaviourManager : BetterMonoBehaviour
	{
		public Vector3 LureOrigin => GetHighestPriority().LureOrigin;

		private readonly Dictionary<AttractScript, int> attractScripts = new Dictionary<AttractScript, int>();

		public SheepMovementSettings sheepSettings;

		private WanderBehaviour wanderBehaviour;
		private AttractBehaviour attractBehaviour;

		private void Awake()
		{
			wanderBehaviour = GetComponent<WanderBehaviour>();
			attractBehaviour = GetComponent<AttractBehaviour>();
			
			ActivateWander();
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

		private void Update()
		{
			if (attractScripts.Any(IsWithinAttractRange))
			{
				if (attractBehaviour.isActiveAndEnabled)
				{
					return;
				}

				ActivateAttract();
			}
			else
			{
				if (wanderBehaviour.isActiveAndEnabled)
				{
					return;
				}

				ActivateWander();
			}
		}

		private void ActivateWander()
		{
			attractBehaviour.enabled = false;
			wanderBehaviour.enabled = true;
		}

		private void ActivateAttract()
		{
			wanderBehaviour.enabled = false;
			attractBehaviour.enabled = true;
		}

		private void OnToggleAttract(ToggleAttractEvent toggleAttractEvent)
		{
			if (toggleAttractEvent.ToggleOn && !attractScripts.ContainsKey(toggleAttractEvent.AttractScript))
			{
				attractScripts.Add(toggleAttractEvent.AttractScript, toggleAttractEvent.Priority);
			}
			else
			{
				attractScripts.Remove(toggleAttractEvent.AttractScript);
			}
		}

		private bool IsWithinAttractRange(KeyValuePair<AttractScript, int> pair)
		{
			return Vector3.Distance(CachedTransform.position, pair.Key.LureOrigin) <= pair.Key.AttractRadius;
		}

		private AttractScript GetHighestPriority()
		{
			AttractScript attractScript = null;
			int priority = int.MinValue;

			// ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
			foreach (KeyValuePair<AttractScript, int> keyValuePair in attractScripts)
			{
				if (keyValuePair.Value <= priority)
				{
					continue;
				}

				priority = keyValuePair.Value;
				attractScript = keyValuePair.Key;
			}

			return attractScript;
		}
	}
}