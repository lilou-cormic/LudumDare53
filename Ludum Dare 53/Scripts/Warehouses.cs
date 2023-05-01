using Godot;

public partial class Warehouses : Node2DCollection<Warehouse>
{
    [Export] PackedScene Prefab = null;

    public void NewWarehouse(Vector2 globalPosition)
    {
        Warehouse warehouse = this.Instantiate<Warehouse>(Prefab, globalPosition, 0);
        List.Add(warehouse);
    }
}