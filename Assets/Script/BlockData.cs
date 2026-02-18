using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData : MonoBehaviour
{
    private static readonly int[][,] blockData = new int[][,]
    {
        new int[,]
        {
            {0, 0, 1 },
            {0, 0, 1 },
            {1, 1, 1 }
        },
        new int[,]
        {
            {1, 1 },
            {1, 1 }
        },
        new int[,]
        {
            {1, 1, 1 },
            {1, 1, 1 },
            {0, 0, 0 }
        },
        new int[,]
        {
            {1, 1, 1 }
        },
        new int[,]
        {
            {1 },
            {1 },
            {1 }
        },

    };
    static BlockData()
    {
        foreach (var block in blockData)
        {
            ReverseBlock(block);
        }
    }

    public static int[,] Get(int index)
    {
        return blockData[index];
    }

    public static int Length()
    {
        return blockData.Length;
    }

    private static void ReverseBlock(int[,] blockData)
    {
        var rows = blockData.GetLength(0);
        var cols = blockData.GetLength(1);

        for (int r = 0; r < rows / 2; r++)
        {
            var topRow = r;
            var bottomRow = rows - 1 - r;
            for (int c = 0; c < cols; c++)
            {
                var temp = blockData[topRow, c];
                blockData[topRow, c] = blockData[bottomRow, c];
                blockData[bottomRow, c] = temp;
            }
        }
    }
}
