using Godot;
using PurpleCable;
using System.Collections.Generic;

public partial class DragonFactory : PrefabFactory<Dragon>
{
    [Export] bool GoingLeft;

    private Vector2[] _spawnPoints;

    public override void _Ready()
    {
        int childCount = GetChildCount();

        List<Vector2> list = new List<Vector2>(childCount);

        for (int i = 0; i < childCount; i++)
        {
            Node2D child = GetChild<Node2D>(i);

            if (child.Name.ToString().StartsWith("SpawnPoint"))
            {
                list.Add(child.GlobalPosition);
            }
        }

        _spawnPoints = list.ToArray();
    }

    public void SendOffDragon()
    {
        Dragon dragon = CreateItem(_spawnPoints.GetRandom());
        dragon.SetDirection(GoingLeft ? Vector2.Left : Vector2.Right);
    }
}