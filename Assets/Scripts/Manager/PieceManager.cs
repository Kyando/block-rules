using System;
using System.Collections.Generic;
using UnityEngine;


public class PieceManager : MonoBehaviour
{
    public static PieceManager instance { get; private set; }
    public Transform piecesTransformParent;
    [SerializeField] public List<PieceView> gamePieces = new List<PieceView>();


    [SerializeField]
    public Dictionary<BaseMeepleView, PieceView> meeplesDict = new Dictionary<BaseMeepleView, PieceView>();

    void Awake()
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

    private void Start()
    {
        InitializePieceViews();
    }

    public void InitializePieceViews()
    {
        gamePieces.Clear();
        meeplesDict.Clear();
        if (piecesTransformParent is not null)
        {
            for (int i = 0; i < piecesTransformParent.childCount; i++)
            {
                PieceView pieceView = piecesTransformParent.GetChild(i).GetComponent<PieceView>();
                if (pieceView)
                {
                    pieceView.pieceModel.pieceId = i;
                    gamePieces.Add(pieceView);
                }
            }
        }

        foreach (var piece in gamePieces)
        {
            foreach (BaseMeepleView meeple in piece.meepleList)
            {
                meeplesDict.Add(meeple, piece);
            }
        }
    }
}