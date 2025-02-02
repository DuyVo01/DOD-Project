using UnityEngine;

public static class HelperExtension
{
    public static bool IsInRange(this float value, float from, float to)
    {
        if (value >= from && value < to)
        {
            return true;
        }
        return false;
    }
}
