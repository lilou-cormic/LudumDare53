using Godot;

namespace PurpleCable
{
    public class AStarTile
    {
        public AStarTile Parent;

        public Vector2I Position;

        public bool IsWalkable;

        public float Weight;

        public float DistanceToTarget;

        public float Cost;

        public float F => DistanceToTarget != -1 && Cost != -1 ? DistanceToTarget + Cost : -1;

        public Vector2 Center => new Vector2(Position.X * GameUI.TileSize + GameUI.TileSize / 2, Position.Y * GameUI.TileSize + GameUI.TileSize / 2);

        public AStarTile(int x, int y, bool isWalkable, float weight = 1)
        {
            Parent = null;

            Position = new Vector2I(x, y);
            IsWalkable = isWalkable;
            Weight = weight;

            DistanceToTarget = -1;
            Cost = 1;
        }

        public override string ToString()
        {
            return $"{Position}, {(IsWalkable ? "Walkable" : "Blocked")}";
        }
    }
}