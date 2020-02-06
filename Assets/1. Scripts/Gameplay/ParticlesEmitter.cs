using JoystickData;
using UnityEngine;

public class ParticlesEmitter : MonoBehaviour
{
	private ParticleSystem[] particles;
	private uint joystickNumber; 

	void Awake()
	{
		particles = GetComponentsInChildren<ParticleSystem>();
		joystickNumber = GetComponent<JoystickNumber>();
	}

	private void Update()
	{
		if (MovementHandler.GetJoystickAxes(joystickNumber).magnitude > 0)
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