using Godot;

public partial class Agent : Area2D
{
    public Warehouse Warehouse { get; set; }

    public Destination Destination { get; set; }

    public DeliveryState DeliveryState { get; private set; }

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
        }

        // move toward destination
    }

    public void SendOut(Warehouse warehouse, Destination destination)
    {
        Warehouse = warehouse;
        Destination = destination;
        DeliveryState = DeliveryState.HeadingToWarehouse;
    }
}
