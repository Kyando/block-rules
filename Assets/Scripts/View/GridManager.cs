using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Camera camera;
    public GridModel gridModel;
    public CellGridView[,] _gridView;
    public CellGridView cellGridViewPrefab;
    public GameObject blockPrefab;

    [SerializeField] private PieceModel selectedPiece;
    [SerializeField] private List<CellGridView> highlightedCells = new List<CellGridView>();
    [SerializeField] private Vector3 mouseWorldPos;
    [SerializeField] private Vector2Int mousePos;

    void Start()
    {
        gridModel = new GridModel(7, 14);
        camera.transform.position = new Vector3((gridModel.width * 0.5f) - .5f, (gridModel.height * 0.5f) - .5f,
            camera.transform.position.z);

        _gridView = new CellGridView[gridModel.width, gridModel.height];
        for (int y = 0; y < gridModel.height; y++)
        {
            for (int x = 0; x < gridModel.width; x++)
            {
                CellGridModel cellModel = new CellGridModel(new Vector2Int(x, y), true, true);
                _gridView[x, y] = Instantiate(cellGridViewPrefab, this.transform);
                _gridView[x, y].Init(cellModel);
            }
        }

        selectedPiece = new PieceModel();
        selectedPiece.blocks = PieceModel.GetBlockT(selectedPiece);
    }

    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector2Int(Mathf.RoundToInt(mouseWorldPos.x), Mathf.RoundToInt(mouseWorldPos.y));

        while (highlightedCells.Count > 0)
        {
            highlightedCells[0].spriteRenderer.color = Color.gray;
            highlightedCells.RemoveAt(0);
        }

        if (IsPositionInGrid(mousePos))
        {
            bool isValidPiecePosition = true;
            foreach (var block in selectedPiece.blocks)
            {
                Vector2Int blockPos = block.piecePosition + mousePos;
                if (!IsPositionInGrid(blockPos))
                {
                    isValidPiecePosition = false;
                    break;
                }
            }

            foreach (var block in selectedPiece.blocks)
            {
                Vector2Int blockPos = block.piecePosition + mousePos;
                if (IsPositionInGrid(blockPos))
                {
                    _gridView[blockPos.x, blockPos.y].spriteRenderer.color =
                        isValidPiecePosition ? Color.cyan : Color.red;
                    highlightedCells.Add(_gridView[blockPos.x, blockPos.y]);
                }
            }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlaceBlock(0);
            }
        }
    }

    private bool IsPositionInGrid(Vector2Int pos)
    {
        if (pos.x >= transform.position.x && pos.x < transform.position.x + gridModel.width &&
            pos.y >= transform.position.y && pos.y < transform.position.y + gridModel.height)
            return true;
        return false;
    }

    private void UpdatePiecePreview()
    {
    }

    private void PlaceBlock(int selectedColumn)
    {
        // CellGridModel dropCell = GetGridDropCell(column);
        var dropCell = GetPieceDropCell(selectedColumn, selectedPiece);

        // blockModel.cellGridModel = dropCell;
        // if (dropCell is not null)
        // {
        //     Vector3 blockPos = new Vector3(dropCell.gridPosition.x, dropCell.gridPosition.y, 0);
        //     GameObject block = Instantiate(blockPrefab, blockPos, Quaternion.identity);
        //     block.name = "Block " + dropCell.gridPosition.x + "," + dropCell.gridPosition.y;
        //     dropCell.isEmpty = false;
        //     dropCell.blockModel = blockModel;
        // }
    }

    CellGridModel GetPieceDropCell(int column, PieceModel pieceModel)
    {
        Dictionary<int, Vector2Int> pieceCollisionCheckDic = new Dictionary<int, Vector2Int>();
        for (int i = 0; i < pieceModel.blocks.Length; i++)
        {
            Debug.Log(pieceModel.blocks[i].piecePosition);
            Vector2Int blockPos = pieceModel.blocks[i].piecePosition;
            Debug.Log(pieceCollisionCheckDic.ContainsKey(blockPos.x));

            if (pieceCollisionCheckDic.ContainsKey(blockPos.x) && pieceCollisionCheckDic[blockPos.x].y >= blockPos.y)
            {
                Debug.Log("Update blockDict position to " + blockPos.x + "," + blockPos.y);
                pieceCollisionCheckDic.Add(blockPos.x, blockPos);
                continue;
            }

            pieceCollisionCheckDic[blockPos.x] = blockPos;
        }

        CellGridModel outputDropCell = null;
        Vector2Int collidedBlock = Vector2Int.zero;
        foreach (int blockColumn in pieceCollisionCheckDic.Keys)
        {
            int checkColumn = column + blockColumn;
            if (checkColumn >= gridModel.width)
                continue;
            CellGridModel dropCell = GetGridDropCell(checkColumn);
            if (outputDropCell is null || dropCell.gridPosition.y > outputDropCell.gridPosition.y)
            {
                outputDropCell = dropCell;
                collidedBlock = pieceCollisionCheckDic[blockColumn];
            }
        }


        for (int i = 0; i < pieceModel.blocks.Length; i++)
        {
            BlockModel blockModel = pieceModel.blocks[i];
            Vector3Int blockPos = new Vector3Int(blockModel.piecePosition.x + column,
                blockModel.piecePosition.y + outputDropCell.gridPosition.y, 0);
            GameObject block = Instantiate(blockPrefab, blockPos, Quaternion.identity);
            CellGridModel targetCell = gridModel.grid[blockPos.x, blockPos.y];
            block.name = "Block " + targetCell.gridPosition.x + "," + targetCell.gridPosition.y;
            targetCell.isEmpty = false;
            targetCell.blockModel = blockModel;
        }

        return outputDropCell;
    }

    CellGridModel GetGridDropCell(int column)
    {
        CellGridModel currentSelectedCell = null;

        for (int y = gridModel.height - 1; y >= 0; y--)
        {
            CellGridModel cell = gridModel.grid[column, y];
            Debug.Log("isEnabled " + cell.isEnabled);
            Debug.Log("isEmpty " + cell.isEmpty);
            if (!cell.isEnabled || !cell.isEmpty)
            {
                Debug.Log("Selected cell is " + y);
                return currentSelectedCell;
            }

            if (cell.isEmpty)
            {
                currentSelectedCell = cell;
            }
        }

        return currentSelectedCell;
    }
}