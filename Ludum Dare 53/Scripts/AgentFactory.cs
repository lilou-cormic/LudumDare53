using PurpleCable;

public partial class AgentFactory : PrefabFactory<Agent>
{
    public Agent GetAgent()
    {
        return CreateItem(GameManager.HQ.GlobalPosition);
    }
}