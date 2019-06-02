using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public enum SeedType
    {
        TypeA,
        TybeB,
        TypeC,
        Count
    }

    [System.Serializable]
    public struct SeedStats
    {
        public string seedName;
        public float growthTime;
        public float goneBadTime;
    }

    public SeedType myType;

    [ContextMenuItem("Get Stats", "GetStats")]
    public SeedStats myStats;

    [SerializeField]
    private SeedsStatsManager seedManager;

    private void GetStats()
    {
        seedManager = FindObjectOfType<SeedsStatsManager>();
        myStats = seedManager.GetSeedStats(myType);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
