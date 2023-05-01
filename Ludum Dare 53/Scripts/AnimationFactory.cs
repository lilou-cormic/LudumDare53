using Godot;
using PurpleCable;

public partial class AnimationFactory : PrefabFactory<Node2D>
{
    public void GetAnimation(Vector2 globalPosition)
    {
        CreateItem(globalPosition);
    }

    public TNode2D GetAnimation<TNode2D>(Vector2 globalPosition)
        where TNode2D : Node2D
    {
        return CreateItem(globalPosition) as TNode2D;
    }
}