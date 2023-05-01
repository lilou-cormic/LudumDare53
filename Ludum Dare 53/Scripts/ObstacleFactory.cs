using Godot;
using PurpleCable;

public partial class ObstacleFactory : PrefabFactory<Obstacle>
{
    [Export] double SpawnDelay = 15f;

    private Obstacle _obstacle;

    private double _timer = 15f;

    public override void _Ready()
    {
        _timer = SpawnDelay;
    }

    public override void _Process(double delta)
    {
        if (_obstacle == null)
        {
            _timer -= delta;

            if (_timer <= 0)
                Spawn();
        }
    }

    public void Spawn()
    {
        _obstacle = CreateItem();
        _obstacle.Factory = this;

        _timer = SpawnDelay;
    }

    public void OnObstacleDestroyed(Obstacle obstacle)
    {
        if (_obstacle == obstacle)
        {
            _obstacle.Factory = null;
            _obstacle = null;
        }
    }
}