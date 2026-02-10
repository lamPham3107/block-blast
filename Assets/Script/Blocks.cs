using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField] private Block[] blocks;
    private float blockWidth;
    private float cellSize;

    private void Start()
    {
        blockWidth = (float)  Board.Size / blocks.Length;
        cellSize = (float) Board.Size / (Block.size * blocks.Length + blocks.Length + 1);
        Debug.Log("blockWidth: " + blockWidth + " cellSize: " + cellSize);
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].transform.localPosition = new Vector3(blockWidth * (i + 0.5f) , -0.25f - cellSize * 4.0f, 0.0f);
            Debug.Log("Block " + i + " position: " + blocks[i].transform.localPosition);
            blocks[i].transform.localScale = new Vector3(cellSize, cellSize, cellSize);
            blocks[i].Initialize();
        }
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].Show(0);
        }
    }
}
