using UnityEngine;

public class GridView : MonoBehaviour
{
    public GameObject cellPrefab; // A visual representation of a grid cell.
    private GameObject[,] cellViews;

    public void Initialize(int width, int height)
    {
        cellViews = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                cellViews[x, y] = cell;
            }
        }
    }

    public void UpdateCell(int x, int y, bool occupied)
    {
        // Change cell color, display, etc.
        cellViews[x, y].GetComponent<SpriteRenderer>().color = occupied ? Color.black : Color.white;
    }
}