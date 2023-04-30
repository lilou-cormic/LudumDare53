using Godot;

public partial class Map : Node2D
{
    public void OnInputEvent(Node viewport, InputEvent inputEvent, int shapeIndex)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.IsPressed())
        {
            GameManager.HQ.Shoot(mouseButtonEvent.GlobalPosition);
        }
    }
}
