using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [System.Serializable]
    public enum SeedType
    {
        TypeA,
        TybeB,
        TypeC,
        Count,
        Null
    }

    [System.Serializable]
    public struct SeedStats
    {
        public string seedName;
        public float growthTime;
        public float goneBadTime;
        public float seedCost;
        public float cropCost;
    }

    public SeedType myType;

    [ContextMenuItem("Get Stats", "GetStats")]
    public SeedStats myStats;

    [Space]

    [SerializeField]
    private SpriteRenderer myArt;

    [SerializeField]
    private SeedsStatsManager seedManager;

    private InventoryManager inventoryManager;

    private PlayerDetection playerDetection;

    private void GetStats()
    {
        seedManager = FindObjectOfType<SeedsStatsManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        myStats = seedManager.GetSeedStats(myType);
        ChangeColor(myType);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetStats();
        playerDetection = GetComponent<PlayerDetection>();
        playerDetection.onDetection = PickedUp;
    }

    private void PickedUp()
    {
        // send message to inventory managers
        inventoryManager.GetSeed(myType);

        Destroy(gameObject);
    }

    private void ChangeColor(SeedType myType)
    {
        switch (myType)
        {
            case SeedType.TypeA:
                myArt.color = Color.cyan;
                break;
            case SeedType.TybeB:
                myArt.color = Color.yellow;
                break;
            case SeedType.TypeC:
                myArt.color = Color.magenta;
                break;
            default:
                break;
        }
    }
}
