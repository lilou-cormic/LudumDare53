using Godot;
using PurpleCable;

public partial class Agent : Area2D
{
    public const float BaseSpeed = 1f;

    public float CurrentSpeed = BaseSpeed;

    public Warehouse Warehouse { get; private set; }

    public Destination Destination { get; private set; }

    public DeliveryState DeliveryState { get; private set; }

    private MoveAnimation MoveAnimation;

    private Sprite2D Sprite;

    public override void _Ready()
    {
        Sprite = GetChild<Sprite2D>(1);
    }

    public override void _Process(double delta)
    {
        Node2D destination;

        switch (DeliveryState)
        {
            case DeliveryState.StandBy:
                return;

            case DeliveryState.HeadingToWarehouse:
                destination = Warehouse;
                break;

            case DeliveryState.HeadingToDestination:
                destination = Destination;
                break;

            case DeliveryState.HeadingToHQ:
                destination = GameManager.HQ;
                break;

            default:
                return;
        }

        if (MoveAnimation == null || MoveAnimation.IsDoneAnimating)
        {
            if (MoveAnimation?.IsDoneAnimating == true && GlobalPosition.DistanceTo(destination.GlobalPosition) <= 0.01f)
            {
                MoveAnimation = null;

                DeliveryState = (DeliveryState)(((int)DeliveryState + 1) % 4);

                return;
            }

            Vector2? nextPosition = GameManager.GetNextPosition(this, destination);

            if (nextPosition.HasValue)
            {
                Vector2 dir = GlobalPosition.DirectionTo(nextPosition.Value);
                if (dir.X != 0)
                    Sprite.FlipH = dir.X < 0;

                MoveAnimation = new MoveAnimation(this) { EndGlobalPosition = nextPosition.Value, IsLine = true, Duration = 1 / CurrentSpeed };
                MoveAnimation.Start();
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (MoveAnimation != null)
        {
            if (GlobalPosition.DistanceTo(MoveAnimation.EndGlobalPosition) <= 0.01f)
                MoveAnimation.End();

            MoveAnimation.PhysicsProcess(delta);
        }
    }

    public void SendOut(Warehouse warehouse, Destination destination)
    {
        Warehouse = warehouse;
        Destination = destination;

        DeliveryState = DeliveryState.HeadingToWarehouse;
    }
}
