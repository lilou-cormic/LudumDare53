using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class HQ : Area2D, IDestination
{
    public static HQ Instance { get; private set; }

    private List<Agent> _agents;

    private AgentFactory AgentFactory { get; set; }

    public Texture2D BubbleImage { get; private set; }

    public int AgentCounter => _agents.Count;

    private const int BulletBaseCount = 3;

    public int BulletCounter { get; private set; }

    public HQ()
    {
        Instance = this;

        _agents = new List<Agent>();
    }

    public override void _Ready()
    {
        AgentFactory = GetNode<AgentFactory>("AgentFactory");
        BubbleImage = GetNode<Sprite2D>("BubbleSprite").Texture;
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

    public void HireAgent()
    {
        _agents.Add(AgentFactory.GetAgent());

        Stats.OnStatsChanged();
    }

    public void BoostAgentSpeed()
    {
        Agent.IncreaseSpeed();
    }

    public void OnAgentDied(Agent agent)
    {
        _agents.Remove(agent);

        if (_agents.Count == 0)
            GameManager.GameOver();

        Stats.OnStatsChanged();
    }

    public void Shoot(Vector2 target)
    {
        if (Projectile.ActiveBulletCount < BulletCounter)
            GameManager.ProjectileFactory.ShootProjectile(target);
    }

    public void AddBullet()
    {
        BulletCounter++;
    }

    public void BoostBulletSpeed()
    {
        Projectile.IncreaseSpeed();
    }

    public static void ResetStats()
    {
        Instance.BulletCounter = BulletBaseCount;
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
