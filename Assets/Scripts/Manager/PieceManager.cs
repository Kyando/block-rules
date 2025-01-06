using System;
using System.Collections.Generic;
using UnityEngine;


public class PieceManager : MonoBehaviour
{
    public static PieceManager instance { get; private set; }
    [SerializeField] public List<PieceView> gamePieces;

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