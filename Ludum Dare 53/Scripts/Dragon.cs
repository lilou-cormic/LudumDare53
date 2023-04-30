using Godot;

public partial class Dragon : Enemy
{
    private const float Speed = 200f;

    public Vector2 _direction;

    public override void _Ready()
    {
        _direction = Vector2.Right;
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _direction * (float)delta * Speed;

        if (GlobalPosition.X > 2000 || GlobalPosition.X < -300)
        {
            QueueFree();
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;

        if (direction.X < 0)
            Scale = new Vector2(-Scale.X, Scale.Y);
    }
}