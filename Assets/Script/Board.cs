using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Board : MonoBehaviour
{
    public const int Size = 8;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform cellTransform;

    private readonly Cell[,] cells = new Cell[Size, Size];
    private readonly int[,] boardData = new int[Size, Size]; // 0: empty, 1: hover, 2: normal
    private readonly List<Vector2Int> hoverPoints = new();

    private void Start()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                cells[i, j] = Instantiate(cellPrefab, cellTransform);
                cells[i, j].transform.localPosition = new Vector3(i, j, 0.0f);
                cells[i, j].Hide();
            }
        }
    }

    public void Hover(Vector2Int point, int blockDataIndex)
    {
        var blockData = BlockData.Get(blockDataIndex);
        var blockRows = blockData.GetLength(0);
        var blockCols = blockData.GetLength(1);
        unHover();
        HoverPonints(point, blockRows, blockCols, blockData);
        if (hoverPoints.Count > 0)
        {
            Hover();
        }

    }
    private void HoverPonints(Vector2Int point , int blockRows , int blockCols , int[,] blockData)
    {
        for(int r = 0; r < blockRows; r++)
        {
            for(int c = 0; c < blockCols; c++)
            {
                if (blockData[r,c] > 0)
                {
                    var hoverPoint = point + new Vector2Int(c,r);
                    if(!IsValidPoint(hoverPoint))
                    {
                        hoverPoints.Clear();
                        return;
                    }
                    hoverPoints.Add(hoverPoint);
                }
            }
        }
    }
    private bool IsValidPoint(Vector2Int point) {
        if (point.x < 0 || point.x >= Size || point.y < 0 || point.y >= Size) return false;
        if (boardData[point.x, point.y] != 0) return false;
        return true;
    }
    private void Hover()
    {
        foreach (var point in hoverPoints)
        {
            boardData[point.x, point.y] = 1;
            cells[point.x, point.y].Hower();
        }
    }

    private void unHover()
    {
        foreach (var point in hoverPoints)
        {
            boardData[point.x, point.y] = 0;
            cells[point.x, point.y].Hide();
        }
        hoverPoints.Clear();
    }
}
