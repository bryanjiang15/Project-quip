using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
public static class Shuffle
{
    public static void ShuffleList<T>(this IList<T> list)
    {
        var rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}