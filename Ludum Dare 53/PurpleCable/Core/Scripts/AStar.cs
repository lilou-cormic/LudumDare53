using Godot;
using System;
using System.Collections.Generic;

namespace PurpleCable
{
    public class AStar
    {
        private readonly List<List<AStarTile>> _grid;

        private int GridRowCount => _grid[0].Count;

        private int GridColCount => _grid.Count;

        public AStar(List<List<AStarTile>> grid)
        {
            _grid = grid;
        }

        public Stack<AStarTile> FindPath(Vector2 Start, Vector2 End)
        {
            AStarTile start = new AStarTile((int)(Start.X / GameUI.TileSize), (int)(Start.Y / GameUI.TileSize), true);
            AStarTile end = new AStarTile((int)(End.X / GameUI.TileSize), (int)(End.Y / GameUI.TileSize), true);

            Stack<AStarTile> path = new Stack<AStarTile>();
            PriorityQueue<AStarTile, float> openList = new PriorityQueue<AStarTile, float>();
            List<AStarTile> closedList = new List<AStarTile>();
            List<AStarTile> adjacentTiles;
            AStarTile current = start;

            // add start Tile to Open List
            openList.Enqueue(start, start.F);

            while (openList.Count != 0 && !closedList.Exists(x => x.Position == end.Position))
            {
                current = openList.Dequeue();
                closedList.Add(current);
                adjacentTiles = GetAdjacentTiles(current);

                foreach (AStarTile tile in adjacentTiles)
                {
                    if (!closedList.Contains(tile) && tile.IsWalkable)
                    {
                        bool isFound = false;
                        foreach (var openTile in openList.UnorderedItems)
                        {
                            if (openTile.Element == tile)
                                isFound = true;
                        }

                        if (!isFound)
                        {
                            tile.Parent = current;
                            tile.DistanceToTarget = Math.Abs(tile.Position.X - end.Position.X) + Math.Abs(tile.Position.Y - end.Position.Y);
                            tile.Cost = tile.Weight + tile.Parent.Cost;
                            openList.Enqueue(tile, tile.F);
                        }
                    }
                }
            }

            // construct path, if end was not closed return null
            if (!closedList.Exists(x => x.Position == end.Position))
                return null;

            // if all good, return path
            AStarTile temp = closedList[closedList.IndexOf(current)];

            if (temp == null)
                return null;

            do
            {
                path.Push(temp);
                temp = temp.Parent;
            } while (temp != start && temp != null);

            return path;
        }

        private List<AStarTile> GetAdjacentTiles(AStarTile tile)
        {
            List<AStarTile> list = new List<AStarTile>();

            int col = tile.Position.X;
            int row = tile.Position.Y;

            if (row + 1 < GridRowCount)
            {
                list.Add(_grid[col][row + 1]);
            }

            if (row - 1 >= 0)
            {
                list.Add(_grid[col][row - 1]);
            }

            if (col - 1 >= 0)
            {
                list.Add(_grid[col - 1][row]);
            }

            if (col + 1 < GridColCount)
            {
                list.Add(_grid[col + 1][row]);
            }

            return list;
        }

        public AStarTile GetTile(int col, int row)
        {
            return col >= 0 && col < GridColCount && row >= 0 && row < GridRowCount ? _grid[col][row] : null;
        }
    }
}