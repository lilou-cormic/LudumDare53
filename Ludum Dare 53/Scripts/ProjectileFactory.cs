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

public class Linear
{
    private readonly Vector3 r0, v;

    public Linear(Vector3 r0, Vector3 r1)
    {
        this.r0 = r0;
        v = r1 - r0;
    }

    public Vector3 Point(float t) => r0 + t * v;

    public Vector3 GetX(float x) => r0 + (x - r0.X) / v.X * v;
}

public class Trajectory
{
    private readonly float a, b, c;

    private readonly Linear line;

    public float A => a;

    public float B => b;

    public float C => c;

    public Trajectory(Vector3 sp, Vector3 ep, float maxHeight)
    {
        line = new Linear(sp, ep);
        Vector3 ap = (ep + sp) / 2;
        ap.Y = sp.Y > ep.Y ? sp.Y + maxHeight : ep.Y + maxHeight;

        float a1 = -sp.X * sp.X + ap.X * ap.X, b1 = -sp.X + ap.X, c1 = -sp.Y + ap.Y;
        float a2 = -ap.X * ap.X + ep.X * ep.X, b2 = -ap.X + ep.X, c2 = -ap.Y + ep.Y;
        float bm = -(b2 / b1), a3 = bm * a1 + a2, c3 = bm * c1 + c2;
        float a = c3 / a3, b = (c1 - a1 * a) / b1, c = sp.Y - a * sp.X * sp.X - b * sp.X;
        this.a = a; this.b = b; this.c = c;
    }

    public Vector3 Point(float x) { var p = line.GetX(x); p.Y = a * p.X * p.X + b * p.X + c; return p; }
}