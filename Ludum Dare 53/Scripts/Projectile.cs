using Godot;
using PurpleCable;

public partial class Projectile : Node2D
{
    private Area2D _aoe;

    private Sprite2D _graphic;

    public const float BaseSpeed = 200f;

    private static float _currentSpeed = BaseSpeed;

    public static int SpeedCounter = 1;

    public static int ActiveBulletCount { get; private set; }

    private Vector2 _target;

    private float _fullDistance;

    private bool _goingDown = false;

    private Vector2 _positionVelocity = Vector2.Zero;

    private Trajectory _trajectory;

    private bool _hasExploded = false;

    private uint _collisionLayer;

    public override void _Ready()
    {
        _aoe = GetNode<Area2D>("AOE");
        _graphic = GetNode<Sprite2D>("Graphic");

        _collisionLayer = _aoe.CollisionLayer;
        _aoe.CollisionLayer = 0;

        ActiveBulletCount++;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_hasExploded)
        {
            QueueFree();
            return;
        }

        float distance = GlobalPosition.DistanceTo(_target);

        _graphic.Position = new Vector2(_graphic.Position.X, -_trajectory.Point(_fullDistance - distance).Y);

        Vector2 dir = GlobalPosition.DirectionTo(_target);

        GlobalPosition += dir * Mathf.Min(_currentSpeed * (float)delta, distance);

        if (GlobalPosition.DistanceTo(_target) < (GameUI.TileSize * 0.01f))
        {
            GlobalPosition = _target;
            Explode();
        }
    }

    public void SetTarget(Vector2 target)
    {
        _target = target;

        _fullDistance = GlobalPosition.DistanceTo(_target);

        _trajectory = new Trajectory(new Vector3(), Vector3.Right * _fullDistance, 150);
    }

    public void Explode()
    {
        _hasExploded = true;

        ActiveBulletCount--;

        _aoe.CollisionLayer = _collisionLayer;
    }

    public static void IncreaseSpeed()
    {
        SpeedCounter++;
        _currentSpeed += BaseSpeed;

        Stats.OnStatsChanged();
    }

    public static void ResetStats()
    {
        SpeedCounter = 1;
        _currentSpeed = BaseSpeed;

        ActiveBulletCount = 0;
    }
}