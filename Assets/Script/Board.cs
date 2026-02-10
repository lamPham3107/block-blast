using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Board : MonoBehaviour
{
    public const int Size = 8;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform cellTransform;

    private readonly Cell[,] cells = new Cell[Size , Size];

    private void Start()
    {
        for ( int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                cells[i,j] = Instantiate(cellPrefab, cellTransform);
                cells[i, j].transform.localPosition = new Vector3(i , j , 0.0f);
                
            }
        }
    }
}
