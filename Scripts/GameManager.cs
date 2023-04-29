using Godot;
using PurpleCable;

public partial class GameManager : Node2D
{
    public static float MusicVolume = 0;

    public static float SfxVolume = 0;

    private static GameManager _instance;

    public static HQ HQ => HQ.Instance;

    [Export] Warehouse[] Warehouses = null;

    [Export] Destination[] Destinations = null;

    public GameManager()
    {
        _instance = this;
    }

    public override void _Ready()
    {
        ScoreManager.ResetScore();
    }

    public static Destination GetDestination()
    {
        return _instance.Destinations.GetRandom();
    }

    public static Warehouse GetWarehouse(Destination destination)
    {
        Warehouse closestWarehouse = null;
        double minDistance = double.MaxValue;

        foreach (Warehouse warehouse in _instance.Warehouses)
        {
            float distance = destination.GlobalPosition.DistanceTo(warehouse.GlobalPosition);

            if (distance < minDistance)
            {
                closestWarehouse = warehouse;
                minDistance = distance;
            }
        }

        return closestWarehouse;
    }

    public static void GameOver()
    {
        _instance.GetTree().ChangeSceneToFile(@"res://Scenes/GameOver.tscn");
    }
}