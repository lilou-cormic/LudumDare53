using Godot;

public partial class Path : Area2D
{
    public void OnAreaEntered(Area2D area)
    {
        Node parent = area.GetParent();

        if (parent is Tile tile)
        {
            tile.IsOnPath = true;
        }
    }
}