using UnityEngine;
using VDFramework;
using VDFramework.UnityExtensions;

namespace Gameplay
{
	public class LightToggler : BetterMonoBehaviour
	{
		[SerializeField]
		private bool shouldCheckEveryFrame = false;

		private Light[] lights;
		private LightChecker lightChecker;

		private void Start()
		{
			lights = GetComponentsInChildren<Light>();
			if (lights.Length == 0)
			{
				Debug.LogWarning("There are no lights present on this gameobject");
			}

			lightChecker = this.EnsureComponent<LightChecker>();

			CheckForLightSource();
		}

		private void LateUpdate()
		{
			if (shouldCheckEveryFrame)
			{
				CheckForLightSource();
			}
		}

		private void CheckForLightSource()
		{
			ToggleLight(!lightChecker.IsInLight());
		}

		private void ToggleLight(bool enable)
		{
			foreach (Light lightSource in lights)
			{
				lightSource.enabled = enable;
			}
		}
	}
}