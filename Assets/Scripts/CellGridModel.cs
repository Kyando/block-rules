using UnityEngine;

public class CellGridModel
{
    public Vector2Int gridPosition = Vector2Int.zero;
    public bool isEnabled = false;
    public bool isEmpty = false;
    public BlockModel blockModel;

    public CellGridModel(Vector2Int gridPosition, bool isEnabled, bool isEmpty)
    {
        this.gridPosition = gridPosition;
        this.isEnabled = isEnabled;
        this.isEmpty = isEmpty;
    }
}