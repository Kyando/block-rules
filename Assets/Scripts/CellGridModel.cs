using UnityEngine;

public class CellGridModel
{
    public bool isEmpty = false;
    public Vector2 gridPosition = Vector2.zero;
    public Color color = Color.black;

    public CellGridModel(Vector2 gridPosition, Color color)
    {
        this.gridPosition = gridPosition;
        this.color = color;
    }
}