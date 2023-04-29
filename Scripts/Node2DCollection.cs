using Godot;
using System.Collections;
using System.Collections.Generic;

public partial class Node2DCollection<TNode2D> : Node2D, IEnumerable<TNode2D>
    where TNode2D : Node2D
{
    protected static Node2DCollection<TNode2D> Instance { get; private set; }

    protected List<TNode2D> List { get; private set; }

    public Node2DCollection()
    {
        Instance = this;
    }

    public override void _Ready()
    {
        int childCount = GetChildCount();

        List = new List<TNode2D>(childCount);

        for (int i = 0; i < childCount; i++)
        {
            List.Add(GetChild<TNode2D>(i));
        }
    }

    public TNode2D GetRandom() => List.ToArray().GetRandom();

    public IEnumerator<TNode2D> GetEnumerator() => List.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}