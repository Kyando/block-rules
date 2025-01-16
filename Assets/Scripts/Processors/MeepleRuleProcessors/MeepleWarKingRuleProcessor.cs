using System.Collections.Generic;
using Enums;
using Processors.Utils;
using UnityEngine;

public abstract class MeepleWarKingRuleProcessor
{
    public static void ProcessMeepleWarKing(GridModel gridModel, PieceView pieceView, BaseMeepleView meeple)
    {
        if (!pieceView.isOnGrid)
        {
            meeple.SetMeepleState(MeepleState.IDLE);
            return;
        }

        KingdomType warkingKingdomType = pieceView.kingdomType;

        bool isKingdomConnected = GridUtils.IsAllKingdomBlocksConnected(pieceView.kingdomType, gridModel);
        if (!isKingdomConnected)
        {
            meeple.SetMeepleState(MeepleState.ANGRY);
            return;
        }

        List<PieceModel> warkingKingdomTiles = GridUtils.GetPiecesByKingdomType(warkingKingdomType, gridModel);
        foreach (var pieceModel in warkingKingdomTiles)
        {
            var neighborKingdomTypes = GridUtils.GetNeighborKingdomTypes(pieceModel, gridModel);
            foreach (var kingdomType in neighborKingdomTypes)
            {
                if (kingdomType != warkingKingdomType)
                {
                    meeple.SetMeepleState(MeepleState.ANGRY);
                    return;
                }
            }
        }

        meeple.SetMeepleState(MeepleState.IDLE);
    }
}