using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class HQ : Area2D
{
    public static HQ Instance { get; private set; }

    private AgentFactory AgentFactory { get; }

    public int MaxAgents { get; private set; } = 10;

    private List<Agent> _agents = new List<Agent>();

    public HQ()
    {
        Instance = this;

        AgentFactory = GetChild<AgentFactory>(2);
    }

    private void NewDelivery()
    {
        Destination destination = GameManager.GetDestination();
        Warehouse warehouse = GameManager.GetWarehouse(destination);

        _agents.FirstOrDefault(agent => agent.DeliveryState == DeliveryState.StandBy)?.SendOut(warehouse, destination);
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
}
