using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle 
{
    public static bool ArraysAreEqual(Sprite[] array1, Sprite[] array2)
    {
        if (array1 == null || array2 == null)
            return false;

        if (array1.Length != array2.Length)
            return false;

        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
                return false;
        }

        return true;
    }
}
