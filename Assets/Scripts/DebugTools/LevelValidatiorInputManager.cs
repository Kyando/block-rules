using Enums;
using UnityEngine;

public class LevelValidatiorInputManager : MouseInputManager
{
    public PieceView piecePrefab;
    public Transform piecesTransformParent;

    private void Awake()
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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSelectedPiecePosition();

        if (Input.GetMouseButtonDown(1))
        {
            if (selectedPiece is not null)
            {
                PieceView piece = selectedPiece;
                OnPieceDeselected();
                Destroy(piece.gameObject);
            }
        }

        HandleInputs();
    }

    private void UpdateSelectedPiecePosition()
    {
        if (selectedPiece is not null)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0);
        }
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
        }
    }


    private void SetTileKingdomType()
    {
        if (selectedPiece is null)
        {
            GenerateBasePiece();
        }
    }

    private void GenerateBasePiece()
    {
        selectedPiece = Instantiate(piecePrefab, piecesTransformParent);
        UpdateSelectedPiecePosition();
    }
}