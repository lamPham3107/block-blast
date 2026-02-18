using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Board : MonoBehaviour
{
    public const int Size = 8;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform cellTransform;

    private readonly Cell[,] cells = new Cell[Size, Size];
    private readonly int[,] boardData = new int[Size, Size]; // 0: empty, 1: hover, 2: normal
    private readonly List<Vector2Int> hoverPoints = new();
    private readonly List<int> fullLineCols = new();
    private readonly List<int> fullLineRows = new();

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
        unHightLight();
        HoverPonints(point, blockRows, blockCols, blockData);
        if (hoverPoints.Count > 0)
        {
            Hover();
            Higtlight(point, blockRows, blockCols);
        }

    }
    private void HoverPonints(Vector2Int point, int blockRows, int blockCols, int[,] blockData)
    {
        for (int r = 0; r < blockRows; r++)
        {
            for (int c = 0; c < blockCols; c++)
            {
                if (blockData[r, c] > 0)
                {
                    var hoverPoint = point + new Vector2Int(c, r);
                    if (!IsValidPoint(hoverPoint))
                    {
                        hoverPoints.Clear();
                        return;
                    }
                    hoverPoints.Add(hoverPoint);
                }
            }
        }
    }
    private bool IsValidPoint(Vector2Int point)
    {
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

    public bool Place(Vector2Int point, int blockDataIndex)
    {
        var blockData = BlockData.Get(blockDataIndex);
        var blockRows = blockData.GetLength(0);
        var blockCols = blockData.GetLength(1);
        unHover();
        HoverPonints(point, blockRows, blockCols, blockData);
        if (hoverPoints.Count > 0)
        {
            Place(point, blockCols, blockRows);
            return true;
        }
        return false;
    }
    private void Place(Vector2Int point, int cols, int rows)
    {
        foreach (var hoverPoint in hoverPoints)
        {
            boardData[hoverPoint.x, hoverPoint.y] = 2;
            cells[hoverPoint.x, hoverPoint.y].Normal();
        }
        CLearFullLine(point, cols, rows);
        hoverPoints.Clear();
    }
    private void CLearFullLine(Vector2Int point, int cols, int rows)
    {
        int toCol = Mathf.Min(point.x + cols - 1, Size - 1);
        int toRow = Mathf.Min(point.y + rows - 1, Size - 1);
        int fromCol = Mathf.Max(point.x, 0);
        int fromRow = Mathf.Max(point.y, 0);
        FullLineCol(fromCol, toCol);
        FullLineRow(fromRow, toRow);
        ClearFullCols();
        ClearFullRows();
    }
    private void FullLineCol(int fromCol, int toCol)
    {
        fullLineCols.Clear();
        for (int c = fromCol; c <= toCol; c++)
        {
            bool isFull = true;
            for (int r = 0; r < Size; r++)
            {
                if (boardData[c, r] != 2)
                {
                    isFull = false;
                    break;
                }
            }
            if (isFull)
            {
                fullLineCols.Add(c);
            }
        }
    }
    private void FullLineRow(int fromRow, int toRow)
    {
        fullLineRows.Clear();
        for (int r = fromRow; r <= toRow; r++)
        {
            var isFull = true;
            for (int c = 0; c < Size; c++)
            {
                if (boardData[c, r] != 2)
                {
                    isFull = false;
                    break;
                }
            }
            if (isFull)
            {
                fullLineRows.Add(r);
            }
        }
    }

    private void ClearFullRows()
    {
        foreach (var row in fullLineRows)
        {
            for (int c = 0; c < Size; c++)
            {
                boardData[c, row] = 0;
                cells[c, row].Hide();
            }
        }
        fullLineRows.Clear();
    }
    private void ClearFullCols()
    {
        foreach (var col in fullLineCols)
        {
            for (int r = 0; r < Size; r++)
            {
                boardData[col, r] = 0;
                cells[col, r].Hide();
            }
        }
        fullLineCols.Clear();
    }
    private void Higtlight(Vector2Int point, int rows, int cols)
    {
        int toCol = Mathf.Min(point.x + cols - 1, Size - 1);
        int toRow = Mathf.Min(point.y + rows - 1, Size - 1);
        int fromCol = Mathf.Max(point.x, 0);
        int fromRow = Mathf.Max(point.y, 0);
        PredictFullLineCol(fromCol, toCol);
        PredictFullLineRow(fromRow, toRow);
        HighlightFullCols();
        HightlightFullRows();
    }

    private void HightlightFullRows()
    {
        foreach (var r in fullLineRows)
        {
            for (int c = 0; c < Size; c++)
            {
                if (boardData[c, r] == 2)
                {
                    cells[c, r].Highlighted();
                }
            }
        }
    }

    private void HighlightFullCols()
    {
        foreach (var c in fullLineCols)
        {
            for (int r = 0; r < Size; r++)
            {
                if (boardData[c, r] == 2)
                {
                    cells[c, r].Highlighted();
                }
            }
        }

    }

    private void PredictFullLineCol(int fromCol, int toCol)
    {
        fullLineCols.Clear();
        for (int c = fromCol; c <= toCol; c++)
        {
            bool isFull = true;
            for (int r = 0; r < Size; r++)
            {
                if (boardData[c, r] != 2 && boardData[c, r] != 1)
                {
                    isFull = false;
                    break;
                }
            }
            if (isFull)
            {
                fullLineCols.Add(c);
            }
        }
    }

    private void PredictFullLineRow(int fromRow, int toRow)
    {
        
        fullLineRows.Clear();
        for (int r = fromRow; r <= toRow; r++)
        {
            bool isFull = true;
            for (int c = 0; c < Size; c++)
            {
                if (boardData[c, r] != 2 && boardData[c, r] != 1)
                {
                    isFull = false;
                    break;
                }
            }
            if (isFull)
            {
                fullLineRows.Add(r);
            }
        }
    }

    private void unHightLight()
    {
        UnHighlightFullCols();
        UnHightlightFullRows();
    }
    private void UnHightlightFullRows()
    {
        foreach (var r in fullLineRows)
        {
            for (int c = 0; c < Size; c++)
            {
                if (boardData[c, r] == 2)
                {
                    cells[c, r].Normal();
                }
            }
        }
    }

    private void UnHighlightFullCols()
    {
        foreach (var c in fullLineCols)
        {
            for (int r = 0; r < Size; r++)
            {
                if (boardData[c, r] == 2)
                {
                    cells[c, r].Normal();
                }
            }
        }

    }
    public bool CheckLose(int blockDataIndex)
    {
        var blockData = BlockData.Get(blockDataIndex);
        var blockRows = blockData.GetLength(0);
        var blockCols = blockData.GetLength(1);
        for (int r = 0; r <= Size - blockRows; r++)
        {
            for (int c = 0; c <= Size - blockCols; c++)
            {
                
                if (CheckPlace(c, r, blockCols, blockRows, blockData))
                {
                    return false;
                }
            }
        }
        return true;
    }
    // kiem tra xem co the dat block vao vi tri (BoardCol, BoardRow) tren board khong
    private bool CheckPlace(int BoardCol, int BoardRow, int BlockCol, int BlockRow, int[,] blockData)
    {
        for (int r = 0; r < BlockRow; r++)
        {
            for (int c = 0; c < BlockCol; c++)
            {
                if (blockData[r, c] != 0 && boardData[BoardCol + c , BoardRow + r]  == 2)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
