namespace Common
{
    using System;
    using System.Collections;
    using UnityEngine;
    using HuaweiARInternal;
    public class DeviceChanged : MonoBehaviour
    {
        public static event Action<int, int> OnDeviceChange;
        public static float CheckDelay = 0.06f;        // How long to wait until we check again.

        static Vector2Int resolution;                    // Current Resolution
        static ScreenOrientation orientation;        // Current Device Orientation
        static bool isAlive = true;                    // Keep this script running?

        void Start()
        {
            StartCoroutine(CheckForChange());
        }

        IEnumerator CheckForChange()
        {
            resolution = new Vector2Int(Screen.width, Screen.height);
            orientation = Screen.orientation;

            while (isAlive)
            {

                // Check for a Resolution Change
                if (resolution.x != Screen.width || resolution.y != Screen.height)
                {
                    resolution = new Vector2Int(Screen.width, Screen.height);
                    if (OnDeviceChange != null) OnDeviceChange(resolution.x, resolution.y);
                }

                // Check for an Orientation Change
                if (orientation != Screen.orientation)
                {
                    orientation = Screen.orientation;
                    if (OnDeviceChange != null) OnDeviceChange(resolution.x, resolution.y);
                }
                yield return new WaitForSeconds(CheckDelay);
            }
        }

        void OnDestroy()
        {
            isAlive = false;
        }

    }
}
