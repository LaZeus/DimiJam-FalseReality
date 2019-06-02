using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    // [x, 0] for seeds
    // [x, 1] for crops
    public int[,] myInventory;

    [SerializeField]
    private TextMeshProUGUI[] seedsUI;

    private SeedsStatsManager seedsStats;
    private string[] seedNames;

    // Start is called before the first frame update
    void Start()
    {
        InitializeInventory();
        UpdateSeedsUI();
    }

    private void InitializeInventory()
    {
        seedsStats = GetComponent<SeedsStatsManager>();
        seedNames = seedsStats.GetSeedNames();

        myInventory = new int[(int)Seed.SeedType.Count, 2];

        for (int i = 0; i < myInventory.GetLength(0); i++)
            for (int j = 0; j < myInventory.GetLength(1); j++)
                myInventory[i, j] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetSeed(Seed.SeedType seed)
    {
        // add seed
        int index = (int)seed;

        myInventory[index, 0]++;

        UpdateSeedsUI();
    }

    public void GetCrop(Seed.SeedType seed)
    {
        // add seed to intentory
        // destroy this
    }

    private void UpdateSeedsUI()
    {
        for (int i = 0; i < myInventory.GetLength(0); i++)
        {
            seedsUI[i].text = seedNames[i] + ": " + myInventory[i, 0];
        }
    }
}
