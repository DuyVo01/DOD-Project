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

    public static bool IsAnyOfThemAreDifferent(params float[] values)
    {
        if (values.Length % 2 != 0)
        {
            throw new System.Exception("Number of values must be even");
        }

        for (int i = 1; i < values.Length; i = i + 2)
        {
            if (values[i] != values[i - 1])
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsNull<T>(this T @object)
        where T : UnityEngine.Object
    {
        return @object == null;
    }

    public static bool IsNotNull<T>(this T @object)
        where T : UnityEngine.Object
    {
        return @object != null;
    }
}
