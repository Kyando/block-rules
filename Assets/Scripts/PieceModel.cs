using UnityEngine;

public class PieceModel
{
    public Vector3 position;
    public BlockModel[] blocks;
    public int pieceType = 0;
    public int pieceId = 0;


    public static BlockModel[] GetBlockT(PieceModel pieceModel)
    {
        return new[]
        {
            new BlockModel(new Vector2Int(0, 0), pieceModel),
            new BlockModel(new Vector2Int(1, 0), pieceModel),
            new BlockModel(new Vector2Int(1, 1), pieceModel),
            new BlockModel(new Vector2Int(2, 0), pieceModel),
        };
    }
}