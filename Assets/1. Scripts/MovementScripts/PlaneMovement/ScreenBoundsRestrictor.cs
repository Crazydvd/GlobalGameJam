using UnityEngine;
using VDUnityFramework.BaseClasses;

namespace MovementScripts.PlaneMovement
{
	public class ScreenBoundsRestrictor : BetterMonoBehaviour
	{
		[Header("RightX, LeftX, TopY, BottomY")] [SerializeField]
		private Vector4 margin = new Vector4(30, 30, 60, 5);

		private Camera mainCamera;

		private void Awake()
		{
			mainCamera = Camera.main;
		}

		public bool IsOutOfBounds(Vector3 position, Vector3 velocity)
		{
			Vector3 screenPos =
				mainCamera.WorldToScreenPoint(position +
											  velocity);
			
			if (screenPos.x > Screen.width - margin.x)
			{
				return true;
			}

			if (screenPos.x < margin.y)
			{
				return true;
			}

			if (screenPos.y > Screen.height - margin.z)
			{
				return true;
			}

			if (screenPos.y < margin.w)
			{
				return true;
			}

			return false;
		}
	}
}