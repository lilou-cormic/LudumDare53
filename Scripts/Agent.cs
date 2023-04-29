using Godot;

public partial class Agent : Area2D
{
    public const float BaseSpeed = 100f;

    public float CurrentSpeed = BaseSpeed;

    public Warehouse Warehouse { get; set; }

    public Destination Destination { get; set; }

    public DeliveryState DeliveryState { get; private set; }

    private Vector2 _positionVelocity = Vector2.Zero;

    public override void _Process(double delta)
    {
        Vector2 destination;

        switch (DeliveryState)
        {
            case DeliveryState.StandBy:
                return;

            case DeliveryState.HeadingToWarehouse:
                destination = Warehouse.GlobalPosition;
                break;

            case DeliveryState.HeadingToDestination:
                destination = Destination.GlobalPosition;
                break;

            case DeliveryState.HeadingToHQ:
                destination = GameManager.HQ.GlobalPosition;
                break;

            default:
                return;
        }

        Vector2 dir = GlobalPosition.DirectionTo(destination);
        GlobalRotation = -Mathf.Atan2(dir.X, dir.Y);

        GlobalPosition = VectorEx.SmoothDamp(GlobalPosition, destination, ref _positionVelocity, 1f, delta, CurrentSpeed);

        if (GlobalPosition.DistanceTo(destination) < 1f)
        {
            DeliveryState = (DeliveryState)(((int)DeliveryState + 1) % 4);
        }
    }

    public void SendOut(Warehouse warehouse, Destination destination)
    {
        Warehouse = warehouse;
        Destination = destination;
        DeliveryState = DeliveryState.HeadingToWarehouse;
    }
}
