using JoystickData;
using UnityEngine;

namespace Particles
{
	public class ParticlesEmitter : MonoBehaviour
	{
		private ParticleSystem[] particles;
		private uint joystickNumber;

		private void Awake()
		{
			particles = GetComponentsInChildren<ParticleSystem>();
			joystickNumber = GetComponent<JoystickNumber>();
		}

		private void Update()
		{
			if (JoystickInput.GetAxes(joystickNumber).magnitude > 0)
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
}