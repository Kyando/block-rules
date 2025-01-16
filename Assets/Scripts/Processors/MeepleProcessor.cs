using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Processors
{
    public abstract class MeepleProcessor
    {
        public static void UpdaetMeepleStatus(GridModel gridModel)
        {
            Dictionary<BaseMeepleView, PieceView> meeplesDict = PieceManager.instance.meeplesDict;
            foreach (BaseMeepleView meeple in meeplesDict.Keys)
            {
                PieceView pieceView = meeplesDict[meeple];
                UpdateMeeplePiece(pieceView, meeple, gridModel);
            }
        }

        private static void UpdateMeeplePiece(PieceView pieceView, BaseMeepleView meepleView, GridModel gridModel)
        {
            MeepleType meepleType = meepleView.meepleModel.meepleType;

            switch (meepleType)
            {
                case MeepleType.KING:
                    MeepleKingRuleProcessor.ProcessMeepleKing(gridModel, pieceView, meepleView);
                    return;
                case MeepleType.QUEEN:
                    MeepleQueenRuleProcessor.ProcessMeepleQueen(gridModel, pieceView, meepleView);
                    return;
                case MeepleType.VILLAGER:
                    MeepleVillagerRuleProcessor.ProcessMeepleVillager(gridModel, pieceView, meepleView);
                    return;
                case MeepleType.FOREST:
                    MeepleForestRuleProcessor.ProcessMeepleForest(gridModel, pieceView, meepleView);
                    return;
                case MeepleType.LUMBERJACK:
                    MeepleLumberjackRuleProcessor.ProcessMeepleLumberjack(gridModel, pieceView, meepleView);
                    return;
                case MeepleType.WARKING:
                    MeepleWarKingRuleProcessor.ProcessMeepleWarKing(gridModel, pieceView, meepleView);
                    return;
                case MeepleType.NONE:
                    Debug.Log("None");
                    return;

                default:
                    throw new ArgumentException("Unknown Meeple type");
            }
        }
    }
}