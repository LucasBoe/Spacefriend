using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumeranleUtil
{
    public static T First<T>(this T[] array)
    {
        return array[0];
    }

    public static T Last<T>(this T[] array)
    {
        return array[array.Length - 1];
    }
}
