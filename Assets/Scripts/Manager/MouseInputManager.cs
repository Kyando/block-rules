using System;
using UnityEngine;

public class MouseInputManager : MonoBehaviour
{
    public static MouseInputManager instance { get; private set; }
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
                Vector3 rotationEuler = selectedPiece.transform.rotation.eulerAngles;
                selectedPiece.transform.rotation = Quaternion.Euler(0, 0, rotationEuler.z - 90);
                selectedPiece?.pieceModel.RotatePiece(clockwise: true);
            }
        }
    }

    public void OnPieceSelected(PieceView piece)
    {
        if (selectedPiece == piece)
        {
            Debug.Log("Deselected Piece");
            this.selectedPiece = null;
            GridManager.instance.OnPieceSelected(null);
            return;
        }

        Debug.Log("OnPieceSelected");
        this.selectedPiece = piece;
        GridManager.instance.OnPieceSelected(piece.pieceModel);
    }
}