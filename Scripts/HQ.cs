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

		GD.Print("HQ_READY _agents:" + _agents);
		GD.Print("HQ_READY AgentFactory:" + AgentFactory);
	}

	public void NewDelivery()
	{
		Destination destination = Destinations.GetRandomDestination();
		Warehouse warehouse = Warehouses.GetClosestWarehouse(destination);

		_agents.FirstOrDefault(agent => agent.DeliveryState == DeliveryState.StandBy)?.SendOut(warehouse, destination);

		GetTree().CreateTimer(5f).Timeout += () => NewDelivery();
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
		GD.Print("HireAgent _agents:" + _agents);
		GD.Print("HireAgent AgentFactory:" + AgentFactory);

		_agents.Add(AgentFactory.GetAgent());
	}
}
