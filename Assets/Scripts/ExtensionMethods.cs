using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    // Lerp between -1 and 1
    public static Vector3 MyLerp(this Transform transform, Vector3 min, Vector3 max, float factor)
    {
        factor = Mathf.Clamp(factor, -1, 1);
        factor = (factor + 1) / 2;
        return min + (max - min) * factor;
    }
}
