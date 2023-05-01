using System;

public static class Stats
{
    public static event Action StatsChanged;

    public static int AgentCounter => GameManager.HQ.AgentCounter;
    public static int AgentSpeed => Agent.SpeedCounter;
    //public static bool AgentShield => Agent.IsShieldOn;
    public static int BulletCounter => GameManager.HQ.BulletCounter;
    public static int BulletSpeed => Projectile.SpeedCounter;
    //public static bool BulletAOE => Projectile.HasLargerAOE;

    public static void OnStatsChanged()
    {
        StatsChanged?.Invoke();
    }
}
