public class GridModel
{
    private int width;
    private int height;
    private bool[,] gridData; // Tracks occupied cells.

    public GridModel(int width, int height)
    {
        this.width = width;
        this.height = height;
        gridData = new bool[width, height];
    }

    public bool CanPlacePiece(PieceModel piece, int x, int y)
    {
        foreach (var block in piece.BlockCoordinates)
        {
            int gridX = x + block.x;
            int gridY = y + block.y;

            if (gridX < 0 || gridX >= width || gridY < 0 || gridY >= height || gridData[gridX, gridY])
                return false;
        }
        return true;
    }

    public void PlacePiece(PieceModel piece, int x, int y)
    {
        foreach (var block in piece.BlockCoordinates)
        {
            gridData[x + block.x, y + block.y] = true;
        }
    }

    public bool IsLevelComplete()
    {
        // Add logic for checking if the level is complete.
        return false;
    }
}