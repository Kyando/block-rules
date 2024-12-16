using UnityEngine;

public class GameController : MonoBehaviour
{
    public GridView gridView;
    public GameObject piecePrefab; // Prefab for pieces.
    private GridModel gridModel;
    private PieceModel currentPiece;
    private PieceView currentPieceView;

    void Start()
    {
        gridModel = new GridModel(10, 10);
        gridView.Initialize(10, 10);
        SpawnNewPiece();
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TryPlacePiece(mousePosition);
        }
        // Add rotation or dragging logic.
    }

    private void TryPlacePiece(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        if (gridModel.CanPlacePiece(currentPiece, x, y))
        {
            gridModel.PlacePiece(currentPiece, x, y);
            gridView.UpdateCell(x, y, true); // Sync grid and view.
            SpawnNewPiece();
        }
    }

    private void SpawnNewPiece()
    {
        Vector2Int[] pieceCoordinates = new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1) }; // Example piece.
        currentPiece = new PieceModel(pieceCoordinates);

        GameObject pieceObject = Instantiate(piecePrefab, Vector3.zero, Quaternion.identity);
        currentPieceView = pieceObject.GetComponent<PieceView>();
        currentPieceView.Initialize(pieceCoordinates);
    }
}