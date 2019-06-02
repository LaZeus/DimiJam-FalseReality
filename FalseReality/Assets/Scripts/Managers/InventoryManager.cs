using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // [x, 0] for seeds
    // [x, 1] for crops
    public int[,] myInventory;

    // Start is called before the first frame update
    void Start()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
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
    }

    public void GetCrop(Seed.SeedType seed)
    {
        // add seed to intentory
        // destroy this
    }
}
