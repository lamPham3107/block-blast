using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardDataGrid : MonoBehaviour
{
    private static readonly int[][,] boardDataGrid = new int[][,]
    {

        new int[,]
        {
            {1, 1, 0, 0, 0, 0, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 1},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 1, 1, 0, 0, 0},
            {0, 0, 0, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 0, 0, 0, 0, 1, 1}
        },
        
        new int[,]
        {
            {1, 0, 0, 1, 0, 0, 1, 0},
            {0, 0, 1, 0, 0, 1, 0, 0},
            {0, 1, 0, 0, 1, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 1},
            {0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 1, 0, 0, 0},
            {1, 0, 0, 1, 0, 0, 0, 1},
            {0, 0, 1, 0, 0, 1, 0, 0}
        },
        
        new int[,]
        {
            {1, 1, 0, 0, 0, 0, 0, 1},
            {0, 0, 0, 1, 0, 0, 0, 0},
            {0, 1, 0, 0, 0, 1, 1, 0},
            {0, 0, 0, 0, 1, 0, 0, 0},
            {0, 0, 1, 0, 0, 0, 1, 0},
            {1, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 1, 0, 1, 1, 0},
            {1, 0, 1, 0, 0, 0, 0, 1}
        },
        
        new int[,]
        {
            {0, 1, 0, 0, 1, 0, 1, 0},
            {1, 0, 0, 0, 0, 1, 0, 0},
            {0, 0, 1, 0, 0, 0, 0, 1},
            {0, 0, 0, 1, 0, 0, 1, 0},
            {1, 0, 0, 0, 1, 0, 0, 0},
            {0, 0, 1, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 1, 0, 1},
            {1, 0, 0, 1, 0, 0, 0, 0}
        },
        new int[,]
        {
            {1, 1, 0, 0, 0, 0, 1, 0},
            {1, 0, 0, 1, 1, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 1},
            {0, 1, 0, 0, 0, 0, 1, 0},
            {0, 0, 0, 0, 0, 1, 0, 0},
            {1, 0, 0, 1, 0, 0, 0, 1},
            {0, 0, 1, 0, 1, 0, 0, 1},
            {0, 1, 0, 0, 1, 1, 0, 0}
        },
        
        new int[,]
        {
            {0, 1, 0, 1, 0, 0, 1, 0},
            {1, 0, 0, 0, 1, 0, 0, 1},
            {0, 0, 1, 0, 0, 1, 0, 0},
            {0, 1, 0, 0, 0, 0, 1, 0},
            {1, 0, 0, 1, 0, 0, 0, 0},
            {0, 0, 1, 0, 1, 0, 0, 1},
            {0, 1, 0, 0, 0, 1, 0, 0},
            {1, 0, 0, 1, 0, 0, 1, 0}
        },
        
        new int[,]
        {
            {1, 0, 0, 0, 1, 0, 1, 0},
            {0, 0, 1, 0, 0, 1, 0, 0},
            {0, 1, 0, 0, 0, 0, 0, 1},
            {0, 0, 0, 1, 0, 0, 1, 0},
            {1, 0, 0, 0, 1, 0, 0, 0},
            {0, 1, 0, 0, 0, 1, 0, 1},
            {0, 0, 1, 0, 0, 0, 1, 0},
            {1, 0, 0, 1, 0, 0, 0, 1}
        },
        
        new int[,]
        {
            {1, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 1, 0, 1, 0},
            {0, 0, 1, 0, 0, 0, 0, 1},
            {0, 0, 0, 0, 0, 1, 0, 0},
            {0, 1, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 1, 0, 0, 1, 0},
            {0, 0, 0, 0, 1, 0, 0, 1},
            {0, 1, 1, 0, 0, 1, 0, 0}
        },
    };
    static BoardDataGrid()
    {
        foreach (var grid in boardDataGrid)
        {
            ReverseBoard(grid);
        }
    }

    public static int[,] Get(int index)
    {
        return boardDataGrid[index];
    }

    public static int Length()
    {
        return boardDataGrid.Length;
    }

    private static void ReverseBoard(int[,] boardData)
    {
        var rows = boardData.GetLength(0);
        var cols = boardData.GetLength(1);

        for (int r = 0; r < rows / 2; r++)
        {
            var topRow = r;
            var bottomRow = rows - 1 - r;
            for (int c = 0; c < cols; c++)
            {
                var temp = boardData[topRow, c];
                boardData[topRow, c] = boardData[bottomRow, c];
                boardData[bottomRow, c] = temp;
            }
        }
    }

}
