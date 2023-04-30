using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class HQ : Area2D
{
    public static HQ Instance { get; private set; }

    private List<Agent> _agents;

    private AgentFactory AgentFactory { get; set; }

    public int MaxAgents { get; private set; } = 10;

    public HQ()
    {
        Instance = this;

        _agents = new List<Agent>();
    }

    public override void _Ready()
    {
        AgentFactory = GetNode<AgentFactory>("AgentFactory");
    }

    public void NewDelivery()
    {
        Destination destination = GameManager.Destinations.GetRandom();

        Warehouse optimalWarehouse = null;
        int minDistance = int.MaxValue;

        for (int i = 0; i < GameManager.Warehouses.Count; i++)
        {
            Warehouse warehouse = GameManager.Warehouses.Get(i);
            int distance = GameManager.GetDistance(this, warehouse) + GameManager.GetDistance(warehouse, destination);

            if (distance > 0 && distance < minDistance)
            {
                minDistance = distance;
                optimalWarehouse = warehouse;
            }
        }

        if (optimalWarehouse != null)
            _agents.FirstOrDefault(agent => agent.DeliveryState == DeliveryState.StandBy)?.SendOut(optimalWarehouse, destination);
    }

    public void IncreaseHiringCapacity()
    {
        MaxAgents += 5;
    }

    public bool CanHireAgent()
    {
        return _agents.Count < MaxAgents;
    }

    public void HireAgent()
    {
        _agents.Add(AgentFactory.GetAgent());
    }

    public void OnAreaEntered(Area2D area)
    {
        Node parent = area.GetParent();

        if (parent is Tile tile)
        {
            tile.IsOnPath = true;
        }
    }
}
