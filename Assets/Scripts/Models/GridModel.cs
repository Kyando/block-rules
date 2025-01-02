using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class GridModel
{
    public int width = 3;
    public int height = 3;
    public CellGridModel[,] grid;

    public GridModel(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new CellGridModel[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[x, y] = new CellGridModel(new Vector2Int(x, y), true, true);
            }
        }
    }
}