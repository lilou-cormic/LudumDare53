using Godot;
using PurpleCable;

public partial class Agent : Node2D
{
    [Export] AudioStream GetPackageSound;

    [Export] AudioStream DeliverySound;

    [Export] AudioStream BackToHQSound;

    [Export] AudioStream DeathSound;

    private const float BaseSpeed = 1f;

    public const float SpeedBump = 1f;

    private static float _currentSpeed = BaseSpeed;

    public static int SpeedCounter = 1;

    public Warehouse Warehouse { get; private set; }

    public Destination Destination { get; private set; }

    public DeliveryState DeliveryState { get; private set; }

    private MoveAnimation MoveAnimation;

    private AnimatedSprite2D _graphic;

    private Sprite2D _bubble;

    private SoundPlayerBehaviour _soundPlayer;

    public override void _Ready()
    {
        _graphic = GetNode<AnimatedSprite2D>("Graphic");
        _bubble = GetNode<Sprite2D>("Bubble");
        _soundPlayer = GetNode<SoundPlayerBehaviour>("SoundPlayer");
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

                switch (DeliveryState)
                {
                    case DeliveryState.StandBy:
                        break;
                    case DeliveryState.HeadingToWarehouse:
                        _soundPlayer.Play(GetPackageSound);
                        break;
                    case DeliveryState.HeadingToDestination:
                        _soundPlayer.Play(DeliverySound);
                        break;
                    case DeliveryState.HeadingToHQ:
                        _soundPlayer.Play(BackToHQSound);
                        break;
                    default:
                        break;
                }

                DeliveryState = (DeliveryState)(((int)DeliveryState + 1) % 4);

                if (DeliveryState == DeliveryState.StandBy)
                    GameManager.ChangeCurrency(100);

                return;
            }

            Vector2? nextPosition = GameManager.GetNextPosition(this, destination);

            if (nextPosition.HasValue)
            {
                Vector2 dir = GlobalPosition.DirectionTo(nextPosition.Value);
                if (dir.X != 0)
                    _graphic.FlipH = dir.X < 0;

                MoveAnimation = new MoveAnimation(this) { EndGlobalPosition = nextPosition.Value, IsLine = true, Duration = 1 / _currentSpeed };
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

        SoundPlayer.Play(DeathSound);

        GameManager.HQ.OnAgentDied(this);
        QueueFree();
    }

    public static void IncreaseSpeed()
    {
        SpeedCounter++;
        _currentSpeed += SpeedBump;

        Stats.OnStatsChanged();
    }

    public static void ResetStats()
    {
        SpeedCounter = 1;
        _currentSpeed = BaseSpeed;
    }
}
