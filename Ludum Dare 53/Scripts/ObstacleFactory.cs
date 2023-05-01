using Godot;
using PurpleCable;

public partial class ObstacleFactory : PrefabFactory<Obstacle>
{
    private Obstacle _obstacle;

    private double _timer = 10f;

    public override void _Ready()
    {
        Spawn();
    }

    public override void _Process(double delta)
    {
        if (_obstacle == null)
        {
            _timer -= delta;

            GD.Print(_timer);

            if (_timer <= 0)
                Spawn();
        }
    }

    public void Spawn()
    {
        _obstacle = CreateItem();
        _obstacle.Factory = this;

        _timer = 15f;
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