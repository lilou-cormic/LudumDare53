using Godot;

public partial class Credits : Node
{
	public void OnBackButtonPressed()
	{   
		GetTree().ChangeSceneToFile(@"res://Scenes/Menu.tscn");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_accept") || Input.IsActionJustPressed("ui_cancel"))
			OnBackButtonPressed();
	}
}
