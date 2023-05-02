using System;

public static class Stats
{
    public static event Action<StatsType> StatsChanged;

    public static int AgentCounter => GameManager.HQ.AgentCounter;
    public static int AgentSpeed => Agent.SpeedCounter;
    public static int BulletCounter => GameManager.HQ.BulletCounter;
    public static int BulletSpeed => Projectile.SpeedCounter;

    public static void OnStatsChanged(StatsType statsType)
    {
        StatsChanged?.Invoke(statsType);
    }
}
