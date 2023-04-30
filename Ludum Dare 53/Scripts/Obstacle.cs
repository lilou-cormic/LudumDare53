using Godot;

public partial class Obstacle : Area2D
{
    public void OnAreaEntered(Area2D area)
    {
        Node parent = area.GetParent();

        if (parent is Tile tile)
        {
            tile.HasObstacle = true;
        }
    }
}
