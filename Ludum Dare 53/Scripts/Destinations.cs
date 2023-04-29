public partial class Destinations : Node2DCollection<Destination>
{
    public static Destination GetRandomDestination()
    {
        return Instance.GetRandom();
    }
}