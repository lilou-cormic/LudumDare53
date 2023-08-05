using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class HQ : Area2D, IDestination
{
    public static HQ Instance { get; private set; }

    private List<Agent> _agents;

    private AgentFactory AgentFactory { get; set; }

    public Texture2D BubbleImage { get; private set; }

    public Node2D _aoeGraphic;

    public int AgentCounter => _agents.Count;

    private AnimationFactory _agentFriedFactory;

    private const int BulletBaseCount = 1;

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

        _agentFriedFactory = GetNode<AnimationFactory>("AgentFriedFactory");

        _aoeGraphic = GetNode<Node2D>("AOEGraphic");
    }

    public override void _Process(double delta)
    {
        _aoeGraphic.GlobalPosition = GetGlobalMousePosition();
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

        Stats.OnStatsChanged(StatsType.AgentCounter);
    }

    public void BoostAgentSpeed()
    {
        Agent.IncreaseSpeed();
    }

    public void OnAgentDied(Agent agent)
    {
        Node2D agentFried = _agentFriedFactory.GetAnimation<Node2D>(agent.GlobalPosition);
        agentFried.GetNode<AnimatedSprite2D>("Graphic").FlipH = agent.GetNode<AnimatedSprite2D>("Graphic").FlipH;

        _agents.Remove(agent);

        if (_agents.Count == 0)
            GameManager.GameOver();

        Stats.OnStatsChanged(StatsType.AgentCounter);
    }

    public void Shoot(Vector2 target)
    {
        if (Projectile.ActiveBulletCount < BulletCounter)
            GameManager.ProjectileFactory.ShootProjectile(target);
    }

    public void AddBullet()
    {
        BulletCounter++;

        Stats.OnStatsChanged(StatsType.BulletCounter);
    }

    public void BoostBulletSpeed()
    {
        Projectile.IncreaseSpeed();
    }

    public void BoostBulletAOE()
    {
        _aoeGraphic.Visible = true;

        Projectile.IncreaseAOE();
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
