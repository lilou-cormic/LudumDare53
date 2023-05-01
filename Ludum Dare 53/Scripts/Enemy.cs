using Godot;

public partial class Enemy : Node2D
{
    public void OnDamagingAreaEntered(Area2D area)
    {
        Node parent = area.GetParent();

        if (parent is Agent agent)
        {
            agent.Damage();
        }
    }

    public void OnBodyAreaEntered(Area2D area)
    {
        Node parent = area.GetParent();

        if (parent is Projectile)
        {
            QueueFree();
        }
    }
}
