using Events.TimerEvents;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.EventSystem;

namespace UI
{
	public class ShowEndScreen : BetterMonoBehaviour
	{
		[SerializeField] private GameObject endScreen = null;
		[SerializeField] private GameObject HUD = null;
		
		private void Awake()
		{
			EventManager.Instance.AddListener<TimerExpiredEvent>(OnTimerExpired);
		}

		private void OnDestroy()
		{
			if (EventManager.IsInitialized)
			{
				EventManager.Instance.RemoveListener<TimerExpiredEvent>(OnTimerExpired);
			}
		}

		private void OnTimerExpired()
		{
			DisplayScreen();
		}

		private void DisplayScreen()
		{
			if (HUD)
			{
				HUD.SetActive(false);
			}
			
			if (endScreen)
			{
				endScreen.SetActive(true);
			}
		}
	}
}