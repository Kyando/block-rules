using Enums;
using Processors.Utils;

public abstract class MeepleForestRuleProcessor
{
    public static void ProcessMeepleForest(GridModel gridModel, PieceView pieceView, BaseMeepleView meeple)
    {
        meeple.SetMeepleState(MeepleState.IDLE);
    }
}