using Godot;
using System;

public partial class Warehouse : Area2D
{
    public void OnAreaEntered(Area2D area)
    {
        Node parent = area.GetParent();

        if (parent is Tile tile)
        {
            tile.SetIsWalkable(true);
        }
    }
}
