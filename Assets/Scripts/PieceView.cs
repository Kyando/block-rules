using UnityEngine;

public class PieceView : MonoBehaviour
{
    public GameObject blockPrefab; // A single block of the piece.
    private GameObject[] blocks;

    public void Initialize(Vector2Int[] coordinates)
    {
        blocks = new GameObject[coordinates.Length];
        for (int i = 0; i < coordinates.Length; i++)
        {
            blocks[i] = Instantiate(blockPrefab, transform);
            blocks[i].transform.localPosition = new Vector3(coordinates[i].x, coordinates[i].y, 0);
        }
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }
}