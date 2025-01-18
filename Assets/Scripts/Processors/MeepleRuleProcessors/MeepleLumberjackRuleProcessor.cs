using Enums;
using Processors.Utils;

public abstract class MeepleLumberjackRuleProcessor
{
    public static void ProcessMeepleLumberjack(GridModel gridModel, PieceView pieceView, BaseMeepleView meeple)
    {
        if (!pieceView.isOnGrid)
        {
            meeple.SetMeepleState(MeepleState.IDLE);
            return;
        }

        BlockView blockView = meeple.blockView;

        bool isMeepleAngry = true;
        var neighborBlocks = GridUtils.GetAdjacentBlocks(blockView.blockModel, gridModel);
        foreach (var blockModel in neighborBlocks)
        {
            if (blockModel.meepleModel is null)
            {
                continue;
            }

            if (blockModel.meepleModel.meepleType == MeepleType.FOREST)
            {
                isMeepleAngry = false;
                break;
            }
        }

        if (isMeepleAngry)
        {
            meeple.SetMeepleState(MeepleState.ANGRY);
        }
        else
        {
            meeple.SetMeepleState(MeepleState.IDLE);
        }
    }
}