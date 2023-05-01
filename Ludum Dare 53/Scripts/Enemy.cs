using Godot;

public partial class Enemy : Node2D
{
    private bool _isDead = false;

    public void OnDamagingAreaEntered(Area2D area)
    {
        if (_isDead)
            return;

        Node parent = area.GetParent();

        if (parent is Agent agent)
        {
            agent.Damage();
        }
    }

    public void OnBodyAreaEntered(Area2D area)
    {
        if (_isDead)
            return;

        Node parent = area.GetParent();

        if (parent is Projectile projectile)
        {
            projectile.Explode(area.GlobalPosition);
            return;
        }

        OnBodyAreaEnteredInternal(parent);
    }

    protected virtual void OnBodyAreaEnteredInternal(Node parent)
    { }

    protected virtual void Die()
    {
        if (_isDead)
            return;

        _isDead = true;

        DieInternal();
    }

    protected virtual void DieInternal()
    {
        QueueFree();
    }
}
