public partial class Warehouses : Node2DCollection<Warehouse>
{
    public static Warehouse GetClosestWarehouse(Destination destination)
    {
        Warehouse closestWarehouse = null;
        double minDistance = double.MaxValue;

        foreach (Warehouse warehouse in Instance)
        {
            float distance = destination.GlobalPosition.DistanceTo(warehouse.GlobalPosition);

            if (distance < minDistance)
            {
                closestWarehouse = warehouse;
                minDistance = distance;
            }
        }

        return closestWarehouse;
    }
}