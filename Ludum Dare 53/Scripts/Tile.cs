using Godot;
using PurpleCable;

public partial class Tile : Sprite2D
{
    private AStarTile _AStarTile = null;
    public AStarTile AStarTile
    {
        get
        {
            if (_AStarTile == null)
                _AStarTile = new AStarTile((int)(GlobalPosition.X / GameUI.TileSize), (int)(GlobalPosition.Y / GameUI.TileSize), false);

            return _AStarTile;
        }
    }

    public Tile()
    {
        Modulate = Colors.Transparent;
    }

    public void SetIsWalkable(bool isWalkable)
    {
        AStarTile.IsWalkable = isWalkable;

        //Modulate = isWalkable ? Colors.White : Colors.Transparent;
    }
}