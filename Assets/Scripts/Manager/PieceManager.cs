using System;
using System.Collections.Generic;
using UnityEngine;


public class PieceManager : MonoBehaviour
{
    public static PieceManager instance { get; private set; }
    public Transform piecesTransformParent;
    [SerializeField] public List<PieceView> gamePieces = new List<PieceView>();


    [SerializeField]
    public Dictionary<KingMeepleView, PieceView> meeplesDict = new Dictionary<KingMeepleView, PieceView>();

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

        if (piecesTransformParent is not null)
        {
            gamePieces.Clear();
            for (int i = 0; i < piecesTransformParent.childCount; i++)
            {
                PieceView pieceView = piecesTransformParent.GetChild(i).GetComponent<PieceView>();
                if (pieceView)
                {
                    gamePieces.Add(pieceView);
                }
            }
        }
    }

    private void Start()
    {
        foreach (var piece in gamePieces)
        {
            foreach (KingMeepleView meeple in piece.meepleList)
            {
                meeplesDict.Add(meeple, piece);
            }
        }
    }
}