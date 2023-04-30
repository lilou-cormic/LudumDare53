using Godot;
using PurpleCable;

public partial class ProjectileFactory : PrefabFactory<Projectile>
{
    public void ShootProjectile(Vector2 target)
    {
        Projectile projectile = CreateItem();
        projectile.SetTarget(target);
    }
}