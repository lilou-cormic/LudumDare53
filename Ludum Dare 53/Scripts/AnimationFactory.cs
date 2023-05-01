using Godot;
using PurpleCable;

public partial class AnimationFactory : PrefabFactory<Node2D>
{
    public void GetAnimation(Vector2 globalPosition)
    {
        CreateItem(globalPosition);
    }
}