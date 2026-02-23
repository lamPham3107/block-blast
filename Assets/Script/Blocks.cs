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
        var generateList = new List<int>();

        for (int i = 0; i < BlockData.Length() - 1; i++)
        {
            int blockIndex = GetWeightedRamdomBlock();
            generateList.Add(blockIndex);
        }

        var canPlaceBlock = getBlockCanPlace();
        generateList.Add(canPlaceBlock);
        Shuffle(generateList);
        for (int i = 0; i < blocks.Length; i++)
        {
            blockGenerateIndex[i] = generateList[i];
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
    private int getBlockCanPlace()
    {
        var list = new List<int>();
        for (int i = 0; i < blocks.Length; i++)
        {
            if (!board.CheckLose(blockGenerateIndex[i]))
            {
                list.Add(i);
            }
        }
        var index = Random.Range(0, list.Count);

        if (list.Count == 0)
        {
            return Random.Range(0, BlockData.Length()); ;
        }
        else
        {
            return list[index];
        }
    }
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--) 
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    private int GetWeightedRamdomBlock()
    {
        var totalWeight = 0;
        for (int i = 0; i < BlockData.Length(); i++)
        {
            totalWeight += BlockData.GetWeight(i);
        }
        var randomValue = Random.Range(0, totalWeight);
        var cumulativeWeight = 0;
        for (int i = 0; i < BlockData.Length(); i++)
        {
            cumulativeWeight += BlockData.GetWeight(i);
            if (randomValue < cumulativeWeight)
            {
                return i;
            }
        }
        return BlockData.Length();
    }
}
