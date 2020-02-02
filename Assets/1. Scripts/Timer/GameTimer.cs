using System;
using Events.GameplayEvents;
using Events.TimerEvents;
using UnityEngine;
using VDUnityFramework.BaseClasses;
using VDUnityFramework.EventSystem;

namespace Timer
{
	public class GameTimer : BetterMonoBehaviour
	{
		[Header("Start value of the timer in hh:mm:ss")] [SerializeField]
		private string timerStartValue = "00:05:00";

		[Header("Increase in time per sheep collected hh:mm:ss")] [SerializeField]
		private string timePerSheep = "00:00:05";

		private float secondsLeft;
		private bool isTimerPaused;

		private Action timerAction;

		private void Awake()
		{
			EventManager.Instance.AddListener<TimerStartEvent>(OnTimerStart);
			EventManager.Instance.AddListener<TimerPauseEvent>(OnTimerPause);
			EventManager.Instance.AddListener<TimerResumeEvent>(OnTimerResume);
			EventManager.Instance.AddListener<SheepCapturedEvent>(OnCapturedSheep);
		}

		private void Start()
		{
			OnTimerStart();
		}

		private void OnDestroy()
		{
			if (EventManager.IsInitialized)
			{
				EventManager.Instance.RemoveListener<TimerStartEvent>(OnTimerStart);
				EventManager.Instance.RemoveListener<TimerPauseEvent>(OnTimerPause);
				EventManager.Instance.RemoveListener<TimerResumeEvent>(OnTimerResume);
				EventManager.Instance.RemoveListener<SheepCapturedEvent>(OnCapturedSheep);
			}
		}

		private void FixedUpdate()
		{
			timerAction?.Invoke();
		}

		private static float ConvertToSeconds(string time)
		{
			DateTime timeValue = DateTime.Parse(time);

			return timeValue.Hour * 3600 + timeValue.Minute * 60 + timeValue.Second;
		}

		private void CalculateTimerStartValue()
		{
			secondsLeft = ConvertToSeconds(timerStartValue);
		}

		private void Countdown()
		{
			if (isTimerPaused)
			{
				return;
			}

			secondsLeft -= Time.deltaTime;

			if (secondsLeft > 0)
			{
				return;
			}

			timerAction = null;

			EventManager.Instance.RaiseEvent(new TimerExpiredEvent());
		}

		private void OnTimerStart()
		{
			isTimerPaused = false;

			CalculateTimerStartValue();
			timerAction = Countdown;
		}

		private void OnTimerPause()
		{
			isTimerPaused = true;
		}

		private void OnTimerResume()
		{
			isTimerPaused = false;
		}

		private void OnCapturedSheep(SheepCapturedEvent sheepCapturedEvent)
		{
			secondsLeft += ConvertToSeconds(timePerSheep);
		}

		public float GetSecondsLeft()
		{
			return secondsLeft;
		}
	}
}