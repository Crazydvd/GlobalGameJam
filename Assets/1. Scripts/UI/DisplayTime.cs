using Timer;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(GameTimer))]
    public class DisplayTime : MonoBehaviour
    {
        public Text displayTime;

        private GameTimer timer;
    
        private void Awake()
        {
            timer = GetComponent<GameTimer>();
        }

        // Update is called once per frame
        void Update()
        {
            displayTime.text = Mathf.RoundToInt(timer.GetSecondsLeft()).ToString();
        }
    }
}
