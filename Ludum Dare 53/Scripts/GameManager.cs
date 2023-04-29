using Godot;
using PurpleCable;

public partial class GameManager : Node2D
{
    public static float MusicVolume = 0;

    public static float SfxVolume = 0;

    private static GameManager _instance;

    public static HQ HQ => HQ.Instance;

    public GameManager()
    {
        _instance = this;
    }

    public override void _Ready()
    {
        ScoreManager.ResetScore();

        HQ.HireAgent();
        HQ.NewDelivery();
    }

    public static void GameOver()
    {
        _instance.GetTree().ChangeSceneToFile(@"res://Scenes/GameOver.tscn");
    }
}