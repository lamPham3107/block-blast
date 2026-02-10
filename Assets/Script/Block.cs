using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Block : MonoBehaviour
{
    public const int size = 5;
    [SerializeField] private Cell cellPrefab;
    private readonly Cell[,] cells = new Cell[size, size];

    public void Initialize()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cells[i, j] = Instantiate(cellPrefab, transform);
            }
        }

    }

    public void Show(int BlockDataIndex)
    {
        Hide();
        var blockData = BlockData.Get(BlockDataIndex);
        var rows = blockData.GetLength(0);
        var cols = blockData.GetLength(1);
        var center = new Vector2(cols / 2.0f, rows / 2.0f);
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (blockData[r,c] > 0)
                {
                    cells[r, c].transform.localPosition = new Vector3(c - center.x, r - center.y, 0.0f);
                    cells[r, c].Normal();
                }
            }
        }
    }
    public void Hide()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cells[i, j].Hide();
            }
        }
    }
}
