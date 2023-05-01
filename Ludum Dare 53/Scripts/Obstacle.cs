using Godot;

public partial class Obstacle : Area2D
{
    private Tile _tile;

    public ObstacleFactory Factory { get; set; }

    public void OnAreaEntered(Area2D area)
    {
        Node parent = area.GetParent();

        switch (parent)
        {
            case Tile tile:
                _tile = tile;
                _tile.HasObstacle = true;
                break;

            case Projectile:
                if (_tile != null)
                    _tile.HasObstacle = false;

                Factory?.OnObstacleDestroyed(this);

                QueueFree();
                break;
        }
    }
}
