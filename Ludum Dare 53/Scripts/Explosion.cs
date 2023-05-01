using Godot;
using PurpleCable;

public partial class Explosion : Node2D
{
    private ScaleAnimation _scaleAnimation;

    public override void _Ready()
    {
        Scale = Vector2.Zero;

        _scaleAnimation = new ScaleAnimation(this) { EndLocalScale = Projectile.HasLargerAOE ? Vector2.One * 2 : Vector2.One };
        _scaleAnimation.Start();
    }

    public override void _Process(double delta)
    {
        if (_scaleAnimation.IsDoneAnimating)
            QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {
        _scaleAnimation.PhysicsProcess(delta);
    }
}
