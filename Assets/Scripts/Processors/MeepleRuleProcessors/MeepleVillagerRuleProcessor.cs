using Enums;
using Processors.Utils;

public abstract class MeepleVillagerRuleProcessor
{
    public static void ProcessMeepleVillager(GridModel gridModel, PieceView pieceView, BaseMeepleView meeple)
    {
        if (!pieceView.isOnGrid)
        {
            meeple.SetMeepleState(MeepleState.IDLE);
            return;
        }

        bool isMeepleAngry = false;
        var neighborBlocksDict = GridUtils.GetNeighborMeeples(pieceView.pieceModel, gridModel);
        foreach (var blockModel in neighborBlocksDict.Keys)
        {
            if (blockModel.meepleModel is null)
            {
                continue;
            }

            if (blockModel.meepleModel.meepleType == MeepleType.KING)
            {
                isMeepleAngry = true;
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