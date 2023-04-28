using Godot;
using PurpleCable;

public partial class GameManager : Node2D
{
    #region Data

    public static float MusicVolume = 0;

    public static float SfxVolume = 0;

    #endregion

    private static GameManager _instance;

    private float _speedMultiplier = 2;

    public static float SpeedMultiplier => _instance?._speedMultiplier ?? 1;

    public GameManager()
    {
        _instance = this;
    }

    public override void _Ready()
    {
        ScoreManager.ResetScore();
    }

    public static void SpeedUp()
    {
        _instance._speedMultiplier += 0.25f;
    }

    public static void GameOver()
    {
        _instance.GetTree().ChangeSceneToFile(@"res://Scenes/GameOver.tscn");
    }
}