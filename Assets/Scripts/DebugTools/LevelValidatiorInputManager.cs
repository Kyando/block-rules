using System.Collections.Generic;
using Enums;
using UnityEngine;

public class LevelValidatiorInputManager : MouseInputManager
{
    public PieceView piecePrefab;
    public Transform piecesTransformParent;
    public List<BaseMeepleView> meeplePrefabs;

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
            SetTileMeepleType(meeplePrefabs[0]);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetTileMeepleType(meeplePrefabs[1]);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetTileMeepleType(null);
        }

        // Kingdom colors
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetTileKingdomType(KingdomType.BLUE_KINGDOM);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SetTileKingdomType(KingdomType.RED_KINGDOM);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetTileKingdomType(KingdomType.YELLOW_KINGDOM);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            SetTileKingdomType(KingdomType.NONE);
        }
    }

    private void SetTileMeepleType(BaseMeepleView baseMeeplePrefab)
    {
        if (selectedPiece is null)
        {
            GenerateBasePiece();
        }

        if (selectedPiece is null)
            return;


        while (selectedPiece.meepleList.Count > 0)
        {
            var child = selectedPiece.meepleList[0];
            selectedPiece.meepleList.RemoveAt(0);
            Destroy(child.gameObject);
        }

        if (baseMeeplePrefab is not null)
        {
            BaseMeepleView baseMeeple = Instantiate(baseMeeplePrefab, selectedPiece.transform, false);
            baseMeeple.transform.localPosition = new Vector3(selectedPiece.posOffset.x, selectedPiece.posOffset.y, 0);
            selectedPiece.meepleList.Add(baseMeeple);
        }

        selectedPiece.InitializeMeeplesAndBlocks();
        PieceManager.instance.InitializePieceViews();
    }


    private void SetTileKingdomType(KingdomType kingdomType)
    {
        if (selectedPiece is null)
        {
            GenerateBasePiece();
        }

        if (selectedPiece is null)
            return;

        selectedPiece.kingdomType = kingdomType;
        selectedPiece.pieceModel.SetBlocksKingdomType(kingdomType);
        selectedPiece.pieceColor = ColorConstants.GetPieceColorFromKingdomType(kingdomType);
        selectedPiece.UpdateSpriteColors();
        PieceManager.instance.InitializePieceViews();
    }

    private void GenerateBasePiece()
    {
        PieceView pieceView = Instantiate(piecePrefab, piecesTransformParent);
        // UpdateSelectedPiecePosition();
        OnPieceClicked(pieceView);
    }
}