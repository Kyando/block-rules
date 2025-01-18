using System;
using UnityEngine;

public class MouseInputManager : MonoBehaviour
{
    public static MouseInputManager instance;
    public PieceView selectedPiece;

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
        if (selectedPiece is not null)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0);
        }


        if (Input.GetMouseButtonDown(1))
        {
            if (selectedPiece is not null)
            {
                selectedPiece.RotatePieceClowckwise();
            }
        }
    }

    public void OnPieceClicked(PieceView piece)
    {
        if (selectedPiece == piece)
        {
            GridManager.instance.PlacePieceOnGrid();
            OnPieceDeselected();
            return;
        }

        OnPieceSelected(piece);
    }

    protected void OnPieceSelected(PieceView piece)
    {
        this.selectedPiece = piece;
        this.selectedPiece.isPieceSelected = true;
        GridManager.instance.OnPieceSelected(piece);
    }

    protected void OnPieceDeselected()
    {
        this.selectedPiece.isPieceSelected = false;
        this.selectedPiece = null;
        GridManager.instance.OnPieceSelected(null);
    }
}