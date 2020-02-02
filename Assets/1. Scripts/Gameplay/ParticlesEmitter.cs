using JoystickData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesEmitter : MonoBehaviour
{

    private ParticleSystem Grassparticles;
    private ParticleSystem Hayparticles;

    void Awake()
    {

        Grassparticles = GameObject.Find("Grass").GetComponent<ParticleSystem>();
        Hayparticles = GameObject.Find("Hay").GetComponent<ParticleSystem>();
    }

    private void Update()
    {
       if( MovementHandler.GetJoystickInput(GetComponent<JoystickNumber>()).magnitude > 0);
        {
            Grassparticles.gameObject.SetActive(true);       
        }
        //TODO Get the input of the action
    }
}
