using UnityEngine;

public class PieceModel
{
    public Vector2Int[] BlockCoordinates { get; private set; }

    public PieceModel(Vector2Int[] coordinates)
    {
        BlockCoordinates = coordinates;
    }

    public void Rotate()
    {
        for (int i = 0; i < BlockCoordinates.Length; i++)
        {
            BlockCoordinates[i] = new Vector2Int(-BlockCoordinates[i].y, BlockCoordinates[i].x); // Rotate 90 degrees.
        }
    }
}