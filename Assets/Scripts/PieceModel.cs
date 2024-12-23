using System.IO;
using UnityEngine;

public class PieceModel
{
    public enum PieceType
    {
        I,
        T,
        L,
        S,
        O
    }

    // public Vector3 position;
    public BlockModel[] blocks;
    private PieceType pieceType = PieceType.I;
    public int pieceId = 0;


    public PieceModel(PieceType pieceType)
    {
        this.pieceType = pieceType;
        this.blocks = GetBlocksByPieceType(this);
    }

    public static BlockModel[] GetBlocksByPieceType(PieceModel pieceModel)
    {
        switch (pieceModel.pieceType)
        {
            case PieceType.I:
                return new[]
                {
                    new BlockModel(new Vector2Int(0, -1), pieceModel),
                    new BlockModel(new Vector2Int(0, 0), pieceModel),
                    new BlockModel(new Vector2Int(0, 1), pieceModel),
                    new BlockModel(new Vector2Int(0, 2), pieceModel),
                };
            case PieceType.T:
                return new[]
                {
                    new BlockModel(new Vector2Int(-1, 0), pieceModel),
                    new BlockModel(new Vector2Int(0, 0), pieceModel),
                    new BlockModel(new Vector2Int(0, 1), pieceModel),
                    new BlockModel(new Vector2Int(1, 0), pieceModel),
                };
            case PieceType.L:
                return new[]
                {
                    new BlockModel(new Vector2Int(0, 2), pieceModel),
                    new BlockModel(new Vector2Int(0, 1), pieceModel),
                    new BlockModel(new Vector2Int(0, 0), pieceModel),
                    new BlockModel(new Vector2Int(1, 0), pieceModel),
                };
            case PieceType.S:
                return new[]
                {
                    new BlockModel(new Vector2Int(-1, 0), pieceModel),
                    new BlockModel(new Vector2Int(0, 0), pieceModel),
                    new BlockModel(new Vector2Int(0, 1), pieceModel),
                    new BlockModel(new Vector2Int(1, 1), pieceModel),
                };
            case PieceType.O:
                return new[]
                {
                    new BlockModel(new Vector2Int(1, 0), pieceModel),
                    new BlockModel(new Vector2Int(0, 0), pieceModel),
                    new BlockModel(new Vector2Int(0, 1), pieceModel),
                    new BlockModel(new Vector2Int(1, 1), pieceModel),
                };
        }

        throw new InvalidDataException("Piece type not recognized");
    }
}