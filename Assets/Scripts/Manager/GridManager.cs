using System.Collections.Generic;
using System.Linq;
using Enums;
using NUnit.Framework;
using Processors;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour
{
    public static GridManager instance { get; private set; }

    public Camera mainCamera;
    public Vector2Int gridSize;
    public GridModel gridModel;
    public CellGridView cellGridViewPrefab;
    public GameObject blockPrefab;
    public List<Vector2Int> disabledCellsList = new List<Vector2Int>();
    public GameObject victoryPanel;

    [SerializeField] private CellGridView[,] _gridView;
    [SerializeField] private PieceView selectedPiece;
    private List<CellGridView> highlightedCells = new List<CellGridView>();


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        victoryPanel?.SetActive(false);
        gridModel = new GridModel(gridSize.x, gridSize.y);
        mainCamera.transform.position = new Vector3((gridModel.width * 0.5f) - .5f, (gridModel.height * 0.5f) - .5f,
            mainCamera.transform.position.z);

        _gridView = new CellGridView[gridModel.width, gridModel.height];
        for (int y = 0; y < gridModel.height; y++)
        {
            for (int x = 0; x < gridModel.width; x++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);
                bool isCellEnabled = true;
                foreach (Vector2Int cellPos in disabledCellsList)
                {
                    if (cellPos == gridPosition)
                    {
                        isCellEnabled = false;
                        break;
                    }
                }

                CellGridModel cellModel = gridModel.grid[x, y];
                cellModel.isEnabled = isCellEnabled;
                _gridView[x, y] = Instantiate(cellGridViewPrefab, this.transform);
                _gridView[x, y].Init(cellModel);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        while (highlightedCells.Count > 0)
        {
            highlightedCells[0].spriteRenderer.color = Color.gray;
            highlightedCells.RemoveAt(0);
        }

        if (selectedPiece is not null)
        {
            Vector2Int piecePos = selectedPiece.GetPiecePosition();
            IsPieceOnValidGridPosition(piecePos);
        }
    }

    private bool IsPieceOnValidGridPosition(Vector2Int piecePos)
    {
        // if (!IsPositionInGrid(piecePos))
        // return false;

        bool isValidPiecePosition = true;
        foreach (var block in selectedPiece.pieceModel.blocks)
        {
            Vector2Int blockPos = block.piecePosition + piecePos;
            if (!IsPositionInGrid(blockPos) || !gridModel.grid[blockPos.x, blockPos.y].isEmpty ||
                !gridModel.grid[blockPos.x, blockPos.y].isEnabled)
            {
                isValidPiecePosition = false;
                break;
            }
        }

        foreach (var block in selectedPiece.pieceModel.blocks)
        {
            Vector2Int blockPos = block.piecePosition + piecePos;
            if (IsPositionInGrid(blockPos))
            {
                _gridView[blockPos.x, blockPos.y].spriteRenderer.color =
                    isValidPiecePosition ? Color.cyan : Color.red;
                highlightedCells.Add(_gridView[blockPos.x, blockPos.y]);
            }
        }

        return isValidPiecePosition;
    }

    public void PlacePieceOnGrid()
    {
        if (selectedPiece is null)
            return;
        Vector2Int piecePos = selectedPiece.GetPiecePosition();
        bool isValidPiecePosition = IsPieceOnValidGridPosition(piecePos);

        if (isValidPiecePosition)
        {
            PlacePieceAtMousePosition(piecePos, selectedPiece);
        }
    }

    private void PlacePieceAtMousePosition(Vector2Int basePosition, PieceView pieceView)
    {
        foreach (var blockView in pieceView.blocks)
        {
            Vector2Int cellPos = blockView.blockModel.piecePosition + basePosition;
            CellGridModel targetCell = gridModel.grid[cellPos.x, cellPos.y];
            blockView.blockModel.cellGridModel = targetCell;
            targetCell.isEmpty = false;
            targetCell.blockModel = blockView.blockModel;
        }

        BlockView firstBlock = pieceView.blocks[0];
        var firstCellPos = new Vector3(firstBlock.blockModel.piecePosition.x + basePosition.x,
            firstBlock.blockModel.piecePosition.y + basePosition.y,
            pieceView.transform.position.z);

        Vector3 pieceOffset = firstCellPos - firstBlock.transform.position;
        pieceView.SetIsPieceOnGrid(true);
        pieceView.transform.position += pieceOffset;


        OnGridUpdated();
    }

    private void OnGridUpdated()
    {
        MeepleProcessor.UpdaetMeepleStatus(gridModel);

        bool hasAngryMeeples = HasAnyAngryMeeple();
        bool isBoardFull = IsBoardFull();
        bool isVictory = isBoardFull && !hasAngryMeeples;
        if (isVictory)
        {
            Debug.Log("Victory");
        }

        victoryPanel?.SetActive(isVictory);
        LevelManager.instance.canLoadNextLevel = isVictory;
    }

    private bool HasAnyAngryMeeple()
    {
        foreach (BaseMeepleView meeple in PieceManager.instance.meeplesDict.Keys)
        {
            if (meeple.meepleModel.meepleState == MeepleState.ANGRY)
            {
                Debug.Log("Angry Meeple found");
                return true;
            }
        }

        return false;
    }

    private bool IsPositionInGrid(Vector2Int pos)
    {
        if (pos.x >= transform.position.x && pos.x < transform.position.x + gridModel.width &&
            pos.y >= transform.position.y && pos.y < transform.position.y + gridModel.height)
            return true;
        return false;
    }

    private bool IsBoardFull()
    {
        bool isVictoryCondition = true;
        for (int y = 0; y < gridModel.height; y++)
        {
            for (int x = 0; x < gridModel.width; x++)
            {
                if (gridModel.grid[x, y].isEnabled && gridModel.grid[x, y].isEmpty)
                {
                    isVictoryCondition = false;
                    break;
                }
            }
        }

        return isVictoryCondition;
    }

    CellGridModel GetPieceDropCell(int column, PieceModel pieceModel)
    {
        Dictionary<int, Vector2Int> pieceCollisionCheckDic = new Dictionary<int, Vector2Int>();
        for (int i = 0; i < pieceModel.blocks.Length; i++)
        {
            Vector2Int blockPos = pieceModel.blocks[i].piecePosition;

            if (pieceCollisionCheckDic.ContainsKey(blockPos.x) && pieceCollisionCheckDic[blockPos.x].y >= blockPos.y)
            {
                pieceCollisionCheckDic.Add(blockPos.x, blockPos);
                continue;
            }

            pieceCollisionCheckDic[blockPos.x] = blockPos;
        }

        CellGridModel outputDropCell = null;
        foreach (int blockColumn in pieceCollisionCheckDic.Keys)
        {
            int checkColumn = column + blockColumn;
            if (checkColumn >= gridModel.width)
                continue;
            CellGridModel dropCell = GetGridDropCell(checkColumn);
            if (outputDropCell is null || dropCell.gridPosition.y > outputDropCell.gridPosition.y)
            {
                outputDropCell = dropCell;
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
            if (!cell.isEnabled || !cell.isEmpty)
            {
                return currentSelectedCell;
            }

            if (cell.isEmpty)
            {
                currentSelectedCell = cell;
            }
        }

        return currentSelectedCell;
    }

    private void RemovePieceFromGrid()
    {
        foreach (var blockView in selectedPiece.blocks)
        {
            blockView.blockModel.cellGridModel.isEmpty = true;
            blockView.blockModel.cellGridModel.blockModel = null;
            blockView.blockModel.cellGridModel = null;
        }

        selectedPiece.SetIsPieceOnGrid(false);
        OnGridUpdated();
    }

    public void OnPieceSelected(PieceView piece)
    {
        selectedPiece = piece;
        if (selectedPiece is not null && selectedPiece.isOnGrid)
        {
            RemovePieceFromGrid();
        }
    }
}