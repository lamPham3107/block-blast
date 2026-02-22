using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct BlockInfo
{
    public int[,] shape;
    public int weight;

    public BlockInfo(int[,] shape, int weight)
    {
        this.shape = shape;
        this.weight = weight;
    }
}
public class BlockData : MonoBehaviour
{
    private static readonly BlockInfo[] blockData = new BlockInfo[]
    {
    new BlockInfo(new int[,]
    {
        {1,1,1},
        {0,1,0}
    }, 5), 

    new BlockInfo(new int[,]
    {
        {0,1,0},
        {1,1,1}
    }, 5),

    new BlockInfo(new int[,]
    {
        {1,0},
        {1,1},
        {1,0}
    }, 5),

    new BlockInfo(new int[,]
    {
        {0,1},
        {1,1},
        {0,1}
    }, 5),

    new BlockInfo(new int[,]
    {
        {1,0},
        {1,0},
        {1,1}
    }, 5),

    new BlockInfo(new int[,]
    {
        {0,1},
        {0,1},
        {1,1}
    }, 5),

    new BlockInfo(new int[,]
    {
        {1,1,1},
        {1,0,0}
    }, 5),

    new BlockInfo(new int[,]
    {
        {0,0,1},
        {1,1,1}
    }, 5),


    new BlockInfo(new int[,]
    {
        {1,1,0},
        {0,1,1}
    }, 3),

    new BlockInfo(new int[,]
    {
        {0,1},
        {1,1},
        {1,0}
    }, 3),

    new BlockInfo(new int[,]
    {
        {0,1,1},
        {1,1,0}
    }, 3),

    new BlockInfo(new int[,]
    {
        {1,0},
        {1,1},
        {0,1}
    }, 3),

    new BlockInfo(new int[,]
    {
        {1,1,1,1}
    }, 3),

    new BlockInfo(new int[,]
    {
        {1},
        {1},
        {1},
        {1}
    }, 3),


    new BlockInfo(new int[,]
    {
        {1,1},
        {1,1}
    }, 20),

    new BlockInfo(new int[,]
    {
        {1,1}
    }, 20),

    new BlockInfo(new int[,]
    {
        {1},
        {1}
    }, 20),
    };

    static BlockData()
    {
        foreach (var block in blockData)
        {
            ReverseBlock(block.shape);
        }
    }

    public static int[,] GetShape(int index)
    {
        return blockData[index].shape;
    }

    public static int GetWeight(int index)
    {
        return blockData[index].weight;
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
