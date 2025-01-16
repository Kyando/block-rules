using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

namespace Processors.Utils
{
    public abstract class GridUtils
    {
        public static Dictionary<BlockModel, MeepleModel> GetNeighborMeeples(PieceModel pieceModel,
            GridModel gridModel)
        {
            Dictionary<BlockModel, MeepleModel> blockMeepleDict = new Dictionary<BlockModel, MeepleModel>();

            foreach (BlockModel blockModel in pieceModel.blocks)
            {
                List<BlockModel> adjacentBlocks = GetAdjacentBlocks(blockModel, gridModel);
                foreach (var adjacentBlock in adjacentBlocks)
                {
                    blockMeepleDict[adjacentBlock] = adjacentBlock.meepleModel;
                }
            }

            return blockMeepleDict;
        }

        public static List<BlockModel> GetAdjacentBlocks(BlockModel blockModel, GridModel gridModel)
        {
            int currentPieceId = blockModel.pieceModel.pieceId;
            HashSet<BlockModel> adjacentBlocks = new HashSet<BlockModel>();
            List<Vector2Int> adjacentDirections = new List<Vector2Int>()
            {
                new(-1, 0),
                new(1, 0),
                new(0, -1),
                new(0, 1),
            };
            var basePos = blockModel.cellGridModel.gridPosition;

            foreach (Vector2Int adjacentDirection in adjacentDirections)
            {
                Vector2Int adjacentPos = basePos + adjacentDirection;
                if (adjacentPos.x < 0 || adjacentPos.y < 0 || adjacentPos.x >= gridModel.width ||
                    adjacentPos.y >= gridModel.height)
                {
                    continue;
                }

                CellGridModel adjacentCell = gridModel.grid[adjacentPos.x, adjacentPos.y];
                if (!adjacentCell.isEmpty && currentPieceId != adjacentCell.blockModel.pieceModel.pieceId)
                {
                    adjacentBlocks.Add(adjacentCell.blockModel);
                }
            }

            return adjacentBlocks.ToList();
        }

        public static List<KingdomType> GetNeighborKingdomTypes(PieceModel pieceModel, GridModel gridModel)
        {
            Dictionary<BlockModel, KingdomType> blockMeepleDict = new Dictionary<BlockModel, KingdomType>();

            foreach (BlockModel blockModel in pieceModel.blocks)
            {
                List<BlockModel> adjacentBlocks = GetAdjacentBlocks(blockModel, gridModel);
                foreach (var adjacentBlock in adjacentBlocks)
                {
                    if (adjacentBlock.kingdomType != KingdomType.NONE)
                    {
                        blockMeepleDict[adjacentBlock] = adjacentBlock.kingdomType;
                    }
                }
            }


            return blockMeepleDict.Values.ToList();
        }

        public static List<PieceModel> GetPiecesByKingdomType(KingdomType kingdomType, GridModel gridModel)
        {
            HashSet<PieceModel> piecesSet = new HashSet<PieceModel>();

            for (int x = 0; x < gridModel.width; x++)
            {
                for (int y = 0; y < gridModel.height; y++)
                {
                    CellGridModel cell = gridModel.grid[x, y];
                    if (!cell.isEmpty && cell.blockModel.kingdomType == kingdomType)
                    {
                        piecesSet.Add(cell.blockModel.pieceModel);
                    }
                }
            }

            return piecesSet.ToList();
        }

        public static HashSet<BlockModel> GetAllBlocksByKingdomType(KingdomType kingdomType, GridModel gridModel)
        {
            HashSet<BlockModel> blockSet = new HashSet<BlockModel>();
            for (int x = 0; x < gridModel.width; x++)
            {
                for (int y = 0; y < gridModel.height; y++)
                {
                    if (!gridModel.grid[x, y].isEmpty && gridModel.grid[x, y].blockModel.kingdomType == kingdomType)
                    {
                        blockSet.Add(gridModel.grid[x, y].blockModel);
                    }
                }
            }

            return blockSet;
        }


        public static bool IsAllKingdomBlocksConnected(KingdomType kingdomType, GridModel gridModel)
        {
            HashSet<BlockModel> kingdomBlocks = GetAllBlocksByKingdomType(kingdomType, gridModel);
            HashSet<BlockModel> openedBlocks = new HashSet<BlockModel>();
            HashSet<BlockModel> blocksQueue = new HashSet<BlockModel>();
            int totalConnectedBlocks = kingdomBlocks.Count;
            blocksQueue.Add(kingdomBlocks.First());
            while (blocksQueue.Count > 0)
            {
                var openedBlock = blocksQueue.First();
                blocksQueue.Remove(openedBlock);
                openedBlocks.Add(openedBlock);

                var adjacentBlocks = GetAdjacentBlocks(openedBlock, gridModel);
                foreach (var adjacentBlock in adjacentBlocks)
                {
                    if (adjacentBlock.kingdomType == kingdomType &&
                        !openedBlocks.Contains(adjacentBlock) &&
                        !blocksQueue.Contains(adjacentBlock))
                    {
                        blocksQueue.Add(adjacentBlock);
                    }
                }
            }

            return openedBlocks.Count == totalConnectedBlocks;
        }
    }
}