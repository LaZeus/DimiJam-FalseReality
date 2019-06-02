using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    // [x, 0] for seeds
    // [x, 1] for crops
    public int[,] myInventory;

    private UIManager uiManager;
    private SeedsStatsManager seedsStats;

    public string[] seedNames;

    // Start is called before the first frame update
    void Start()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        seedsStats = GetComponent<SeedsStatsManager>();
        uiManager = GetComponent<UIManager>();
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

    public bool CanPlantSeed(int index)
    {
        return myInventory[index, 0] > 0;
    }

    public void PlantSeed(int index)
    {
        myInventory[index, 0]--;
        uiManager.UpdateSeedsUI();
    }

    public void GetSeed(Seed.SeedType seed)
    {
        // add seed to intentory
        int index = (int)seed;

        myInventory[index, 0]++;

        uiManager.UpdateSeedsUI();
    }

    public void GetCrop(Seed.SeedType seed)
    {
        // add seed to intentory
        int index = (int)seed;

        myInventory[index, 1]++;

        uiManager.UpdateSeedsUI();
    }
}
