using UnityEngine;

namespace UnityUtils
{
    public static class ScreenHelper
    {
        public static bool IsPortrait()
        {
            return Screen.currentResolution.height > Screen.currentResolution.width;
        }
    }
}
