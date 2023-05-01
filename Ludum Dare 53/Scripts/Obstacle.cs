using Godot;
using PurpleCable;

public partial class Obstacle : Area2D
{
    [Export] AudioStream DestroyedSound;

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

            case Explosion:
                if (_tile != null)
                    _tile.HasObstacle = false;

                SoundPlayer.Play(DestroyedSound);

                Factory?.OnObstacleDestroyed(this);

                QueueFree();
                break;
        }
    }
}
