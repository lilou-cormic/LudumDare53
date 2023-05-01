using Godot;
using PurpleCable;

public partial class Agent : Node2D
{
    public const float BaseSpeed = 1f;

    public float CurrentSpeed = BaseSpeed;

    public Warehouse Warehouse { get; private set; }

    public Destination Destination { get; private set; }

    public DeliveryState DeliveryState { get; private set; }

    private MoveAnimation MoveAnimation;

    private AnimatedSprite2D _graphic;

    private Sprite2D _bubble;

    public override void _Ready()
    {
        _graphic = GetNode<AnimatedSprite2D>("Graphic");
        _bubble = GetNode<Sprite2D>("Bubble");
    }

    public override void _Process(double delta)
    {
        Node2D destination;

        switch (DeliveryState)
        {
            case DeliveryState.StandBy:
                _bubble.Texture = null;
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


        _bubble.Texture = ((IDestination)destination).BubbleImage;

        if (MoveAnimation == null || MoveAnimation.IsDoneAnimating)
        {
            if (MoveAnimation?.IsDoneAnimating == true && GlobalPosition.DistanceTo(destination.GlobalPosition) <= 0.01f)
            {
                MoveAnimation = null;

                DeliveryState = (DeliveryState)(((int)DeliveryState + 1) % 4);

                if (DeliveryState == DeliveryState.StandBy)
                    ScoreManager.AddPoints(100);

                return;
            }

            Vector2? nextPosition = GameManager.GetNextPosition(this, destination);

            if (nextPosition.HasValue)
            {
                Vector2 dir = GlobalPosition.DirectionTo(nextPosition.Value);
                if (dir.X != 0)
                    _graphic.FlipH = dir.X < 0;

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

    public void Damage()
    {
        if (DebugHelper.GodMode)
            return;

        GameManager.HQ.OnAgentDied(this);
        QueueFree();
    }
}
