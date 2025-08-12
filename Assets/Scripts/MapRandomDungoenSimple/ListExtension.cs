using System.Collections.Generic;
using System;

public static class ListExtension
{
    public static void Shuffle<T>(this List<T> list)
    {
        Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);  // chọn một chỉ số từ 0 đến n
            (list[n], list[k]) = (list[k], list[n]); // hoán đổi
        }
    }

    public static List<T> Skip<T>(this List<T> list, int index)
    {
        List<T> result = new List<T>();
        for(int i = 0; i < list.Count; i++)
        {
            if(i == index) continue;
            result.Add(list[i]);
        }
        return result;
    }
}
