using Godot;

public partial class MusicPlayer : AudioStreamPlayer
{
    public override void _Ready()
    {
        VolumeDb = GameManager.MusicVolume;
    }
}