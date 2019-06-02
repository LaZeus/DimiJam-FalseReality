using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsStatsManager : MonoBehaviour
{
    [SerializeField]
    [ContextMenuItem("Get Seeds", "GetSeeds")]
    private Seed.SeedStats[] seedStats;

    private void GetSeeds()
    {
        // gets seeds' count
        seedStats = new Seed.SeedStats[(int)Seed.SeedType.Count];

        // gets each seed's name
        for (int i = 0; i < seedStats.Length; i++)
            seedStats[i].seedName = ((Seed.SeedType)i).ToString();
    }

    private Seed.SeedStats GetSeedValues(int i)
    {
        return seedStats[i];
    }

    public Seed.SeedStats GetSeedStats(Seed.SeedType type)
    {
        return GetSeedValues((int)type);
    }

    public string[] GetSeedNames()
    {
        string[] names = new string[seedStats.Length];

        for (int i = 0; i < names.Length; i++)
            names[i] = seedStats[i].seedName;

        return names;
    }
}
