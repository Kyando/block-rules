using System.Collections.Generic;
using System.Linq;
using Enums;
using Processors.Utils;
using UnityEngine;

public class LevelValidatiorInputManager : MouseInputManager
{
    public PieceView piecePrefab;
    public Transform piecesTransformParent;
    public List<BaseMeepleView> meeplePrefabs;
    public List<BlockModel> meepleBlocks;

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
        //Debug Base
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            GridUtils.IsAllKingdomBlocksConnected(KingdomType.BLUE_KINGDOM, GridManager.instance.gridModel);
        }

        //Meeples
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetTileMeepleType(meeplePrefabs[0]);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetTileMeepleType(meeplePrefabs[1]);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SetTileMeepleType(meeplePrefabs[2]);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SetTileMeepleType(meeplePrefabs[3]);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SetTileMeepleType(meeplePrefabs[4]);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SetTileMeepleType(meeplePrefabs[5]);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetTileMeepleType(null);
        }

        // Kingdom colors
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetTileKingdomType(KingdomType.BLUE_KINGDOM);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetTileKingdomType(KingdomType.RED_KINGDOM);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetTileKingdomType(KingdomType.YELLOW_KINGDOM);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
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
        selectedPiece.UpdateColorsBasedOnKingdomType();
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