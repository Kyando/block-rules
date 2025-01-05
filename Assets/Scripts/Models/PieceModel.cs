using System.IO;
using UnityEngine;

public class PieceModel
{
    public enum PieceType
    {
        I,
        T,
        L,
        J,
        S,
        Z,
        O
    }

    // public Vector3 position;
    public BlockModel[] blocks;
    private PieceType pieceType = PieceType.I;
    public Color pieceColor = Color.white  ;
    public int pieceId = 0;


    public PieceModel(PieceType pieceType, Color pieceColor)
    {
        this.pieceType = pieceType;
        this.pieceColor = pieceColor;
        this.blocks = GetBlocksByPieceType(this);
    }

    public void FlipPieceHorizontally()
    {
        foreach (BlockModel block in blocks)
        {
            block.piecePosition.x *= -1;
        }
    }

    public void RotatePiece(bool clockwise = true)
    {
        foreach (BlockModel block in blocks)
        {
            int newX = clockwise ? block.piecePosition.y : -block.piecePosition.y;
            int newY = clockwise ? -block.piecePosition.x : block.piecePosition.x;

            block.piecePosition.x = newX;
            block.piecePosition.y = newY;
        }
    }

    public static BlockModel[] GetBlocksByPieceType(PieceModel pieceModel)
    {
        switch (pieceModel.pieceType)
        {
            case PieceType.I:
                return new[]
                {
                    new BlockModel(new Vector2Int(-1, 0), pieceModel),
                    new BlockModel(new Vector2Int(0, 0), pieceModel),
                    new BlockModel(new Vector2Int(1, 0), pieceModel),
                    new BlockModel(new Vector2Int(2, 0), pieceModel),
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
                    new BlockModel(new Vector2Int(-1, 0), pieceModel),
                    new BlockModel(new Vector2Int(0, 0), pieceModel),
                    new BlockModel(new Vector2Int(1, 0), pieceModel),
                    new BlockModel(new Vector2Int(1, 1), pieceModel),
                };
            case PieceType.J:
                return new[]
                {
                    new BlockModel(new Vector2Int(-1, 1), pieceModel),
                    new BlockModel(new Vector2Int(-1, 0), pieceModel),
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
            case PieceType.Z:
                return new[]
                {
                    new BlockModel(new Vector2Int(1, 0), pieceModel),
                    new BlockModel(new Vector2Int(0, 0), pieceModel),
                    new BlockModel(new Vector2Int(0, 1), pieceModel),
                    new BlockModel(new Vector2Int(-1, 1), pieceModel),
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