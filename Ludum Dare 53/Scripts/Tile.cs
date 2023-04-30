using Godot;
using PurpleCable;

public partial class Tile : Sprite2D
{
    private bool _IsOnPath = false;
    public bool IsOnPath
    {
        get => _IsOnPath;

        set
        {
            if (_IsOnPath != value)
            {
                _IsOnPath = value;
                SetIsWalkable();
            }
        }
    }

    private bool _HasObstacle = false;
    public bool HasObstacle
    {
        get => _HasObstacle;

        set
        {
            if (_HasObstacle != value)
            {
                _HasObstacle = value;
                SetIsWalkable();
            }
        }
    }

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

    private bool _isMouseOver = false;

    public Tile()
    {
        Modulate = Colors.Transparent;
    }

    private void SetIsWalkable()
    {
        AStarTile.IsWalkable = IsOnPath && !HasObstacle;

        if (DebugHelper.ShowWalkableTiles)
            Modulate = AStarTile.IsWalkable ? Colors.White : Colors.Transparent;
    }
}
