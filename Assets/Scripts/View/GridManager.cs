using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Camera camera;
    public GridModel gridModel;
    public CellGridView[,] _gridView;
    public CellGridView cellGridViewPrefab;
    public GameObject blockPrefab;

    void Start()
    {
        gridModel = new GridModel(3, 3);
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Pressed Q");
            BlockModel blockModel = new BlockModel();
            CellGridModel dropCell = GetGridDropCell(0);
            Debug.Log(dropCell);
            blockModel.cellGridModel = dropCell;
            if (dropCell is not null)
            {
                Vector3 blockPos = new Vector3(dropCell.gridPosition.x, dropCell.gridPosition.y, 0);
                GameObject block = Instantiate(blockPrefab, blockPos, Quaternion.identity);
                block.name = "Block " + dropCell.gridPosition.x + "," + dropCell.gridPosition.y;
                dropCell.isEmpty = false;
                dropCell.blockModel = blockModel;
            }
        }
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