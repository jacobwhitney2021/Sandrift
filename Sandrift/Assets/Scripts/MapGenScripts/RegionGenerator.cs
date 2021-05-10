using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RegionGenerator
{
    public static float[,] GenerateOceanMap(int size)
    {
        float[,] map = new float[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                map[i, j] = 1.0f;
            }
        }

        return map;
    }

    public static float[,] GenerateNormalMap(int size)
    {
        float[,] map = new float[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                map[i, j] = 0.0f;
            }
        }

        return map;
    }
}
