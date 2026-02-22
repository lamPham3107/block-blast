using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Block : MonoBehaviour
{
    public const int size = 5;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Board board;
    [SerializeField] private Blocks blocks;
    private SortingGroup sortingGroup;
    private readonly Cell[,] cells = new Cell[size, size];
    private Vector3 preMousePosition;
    private Vector3 position;
    private Vector3 scale;
    private readonly Vector3 offset = new Vector3(0f, 2f, 0f);
    private Vector3 inputPos;
    private Camera cam;
    private Vector2 center;
    private Vector2Int currentDragPoint;
    private Vector2Int previousDragPoint;
    private int blockDataIndex;
    private int currentColorIndex;
    private void Awake()
    {
        cam = Camera.main;
        sortingGroup = gameObject.GetComponent<SortingGroup>();
    }
    public void Initialize()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cells[i, j] = Instantiate(cellPrefab, transform);
            }
        }

        position = transform.position;
        scale = transform.localScale;

    }

    public void Show(int BlockDataIndex)
    {
        // luu chi so block du lieu hien thi
        this.blockDataIndex = BlockDataIndex;
        Hide();
        // hien thi block du lieu tu BlockData
        var blockData = BlockData.GetShape(BlockDataIndex);
        var rows = blockData.GetLength(0);
        var cols = blockData.GetLength(1);
        center = new Vector2(cols / 2.0f, rows / 2.0f);
        currentColorIndex = Random.Range(0, cellPrefab.colorSprites.Length);
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (blockData[r,c] > 0)
                {
                    cells[r, c].transform.localPosition = new Vector3(c - center.x, r - center.y, 0.0f);
                    cells[r, c].SetColor(currentColorIndex);
                    cells[r, c].Normal();
                    Debug.Log("Show cell at " + cells[r, c].transform.localPosition);
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

    private void OnMouseDown()
    {
        // lay vi tri chuot trong the gioi
        inputPos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = position + offset;
        transform.localScale = Vector3.one;
        currentDragPoint = Vector2Int.RoundToInt((Vector2)transform.position - center);
        board.Hover(currentDragPoint, blockDataIndex,currentColorIndex);
        previousDragPoint = currentDragPoint;
        preMousePosition = Input.mousePosition;
        sortingGroup.sortingOrder = 4;
        
    }
    private void OnMouseUp()
    {
        preMousePosition = Vector3.positiveInfinity;
        currentDragPoint = Vector2Int.RoundToInt((Vector2)transform.position - center);
        // neu dat khoi thanh cong thi an di
        if(board.Place(currentDragPoint, blockDataIndex, currentColorIndex))
        {
            gameObject.SetActive(false);
            blocks.Remove();
        }
        transform.position = position;
        transform.localScale = scale;
        sortingGroup.sortingOrder = 0;
    }
    private void OnMouseDrag()
    {
        var curentMousePosition = Input.mousePosition;
        if(curentMousePosition != preMousePosition)
        {
            preMousePosition = curentMousePosition;
            // tinh toan vi tri moi
            var inputDelta = cam.ScreenToWorldPoint(curentMousePosition) - inputPos;
            transform.position = position + offset + inputDelta * 1.2f;
            currentDragPoint = Vector2Int.RoundToInt((Vector2)transform.position - center);
            if(currentDragPoint != previousDragPoint)
            {
                previousDragPoint = currentDragPoint;
                Debug.Log("Drag at " + currentDragPoint);
            }
            board.Hover(currentDragPoint, blockDataIndex, currentColorIndex);
        }

    }
}
