using JoystickData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesEmitter : MonoBehaviour
{
    private ParticleSystem[] particles;

    void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (MovementHandler.GetJoystickAxes(GetComponent<JoystickNumber>()).magnitude > 0)
        {
            foreach (ParticleSystem particle in particles)
            {
                particle.Play();
            }
        }
        else
        {
            foreach (ParticleSystem particle in particles)
            {
                particle.Stop();
            }
        }
    }
}
