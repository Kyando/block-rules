using Enums;
using Processors.Utils;

public abstract class MeepleQueenRuleProcessor
{
    public static void ProcessMeepleQueen(GridModel gridModel, PieceView pieceView, BaseMeepleView meeple)
    {
        if (!pieceView.isOnGrid)
        {
            meeple.SetMeepleState(MeepleState.IDLE);
            return;
        }


        bool isMeepleAngry = true;
        var neighborBlocksDict = GridUtils.GetNeighborMeeples(pieceView.pieceModel, gridModel);
        foreach (var blockModel in neighborBlocksDict.Keys)
        {
            if (blockModel.meepleModel is null)
            {
                continue;
            }

            if (blockModel.meepleModel.meepleCategories.Contains(MeepleCategory.ROYAL))
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