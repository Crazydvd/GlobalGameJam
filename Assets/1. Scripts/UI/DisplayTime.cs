using System.Collections;
using System.Collections.Generic;
using Timer;
using UnityEngine.UI; 
using UnityEngine;

public class DisplayTime : MonoBehaviour
{
    public Text displayTime;
    public GameTimer gameTime; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayTime.text = Mathf.RoundToInt(gameTime.GetSecondsLeft()).ToString();
    }
}
