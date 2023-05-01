using Godot;

public partial class GameOver : Control
{
	public void OnRetryButtonPressed()
	{
		GetTree().ChangeSceneToFile(@"res://Scenes/Main.tscn");
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_accept"))
			OnRetryButtonPressed();

		if (Input.IsActionJustPressed("ui_cancel"))
			OnQuitButtonPressed();
	}
}
