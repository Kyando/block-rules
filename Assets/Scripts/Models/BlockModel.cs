using Enums;
using UnityEngine;

public class BlockModel
{
    public Vector2Int piecePosition;
    public PieceModel pieceModel;
    public CellGridModel cellGridModel;
    public MeepleModel meepleModel;
    // public MeepleType meepleType = MeepleType.NONE;


    public BlockModel(Vector2Int piecePosition, PieceModel pieceModel)
    {
        this.piecePosition = piecePosition;
        this.pieceModel = pieceModel;
    }
}