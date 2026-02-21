using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField] private Block[] blocks;
    private float blockWidth;
    private float cellSize;
    public int blockCount;
    private int[] blockGenerateIndex;
    [SerializeField] private Board board;
    private void Start()
    {
        blockWidth = (float)Board.Size / blocks.Length;
        cellSize = (float)Board.Size / (Block.size * blocks.Length + blocks.Length + 1);
        Debug.Log("blockWidth: " + blockWidth + " cellSize: " + cellSize);
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].transform.localPosition = new Vector3(blockWidth * (i + 0.5f), -0.25f - cellSize * 4.0f, 0.0f);
            Debug.Log("Block " + i + " position: " + blocks[i].transform.localPosition);
            blocks[i].transform.localScale = new Vector3(cellSize, cellSize, cellSize);
            blocks[i].Initialize();
        }
        blockGenerateIndex = new int[blocks.Length];
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blockGenerateIndex[i] = Random.Range(0, BlockData.Length());
            blocks[i].gameObject.SetActive(true);
            blocks[i].Show(blockGenerateIndex[i]);
            blockCount++;
            Debug.Log("Generate block " + i + " with data index: " + blockGenerateIndex[i]);
        }
    }

    public void Remove()
    {
        blockCount--;
        if (blockCount <= 0)
        {
            blockCount = 0;
            Generate();
        }
        var lose = true;
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].gameObject.activeSelf && !board.CheckLose(blockGenerateIndex[i])) 
            {
                lose = false;
                break;
            }
        }
        if (lose)
        {
            GameController.Instance.Lose();
        }
    }

}
