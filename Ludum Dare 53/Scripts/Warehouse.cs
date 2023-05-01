using Godot;

public partial class Warehouse : Area2D, IDestination
{
    public Texture2D BubbleImage { get; private set; }

    public override void _Ready()
    {
        BubbleImage = GetNode<Sprite2D>("BubbleSprite").Texture;
    }

    public void OnAreaEntered(Area2D area)
    {
        Node parent = area.GetParent();

        if (parent is Tile tile)
        {
            tile.IsOnPath = true;
        }
    }
}
