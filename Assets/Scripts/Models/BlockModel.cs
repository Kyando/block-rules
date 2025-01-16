using Enums;
using UnityEngine;

[System.Serializable]
public class BlockModel
{
    public Vector2Int piecePosition;
    public PieceModel pieceModel;
    public CellGridModel cellGridModel;
    public MeepleModel meepleModel;
    public KingdomType kingdomType = KingdomType.NONE;


    public BlockModel(Vector2Int piecePosition, PieceModel pieceModel)
    {
        this.piecePosition = piecePosition;
        this.pieceModel = pieceModel;
    }
}