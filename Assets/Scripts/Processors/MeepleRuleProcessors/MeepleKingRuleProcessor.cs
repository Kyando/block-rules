using Enums;
using Processors.Utils;

public abstract class MeepleKingRuleProcessor
{
    public static void ProcessMeepleKing(GridModel gridModel, PieceView pieceView, BaseMeepleView meeple)
    {
        if (!pieceView.isOnGrid)
        {
            meeple.SetMeepleState(MeepleState.IDLE);
            return;
        }

        bool isKingdomConnected = GridUtils.IsAllKingdomBlocksConnected(pieceView.kingdomType, gridModel);
        
        if (isKingdomConnected)
        {
            meeple.SetMeepleState(MeepleState.IDLE);
        }
        else
        {
            meeple.SetMeepleState(MeepleState.ANGRY);
        }
    }
}