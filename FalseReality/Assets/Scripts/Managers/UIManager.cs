using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Money")]
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [Header("Plant")]

    [SerializeField]
    private TextMeshProUGUI[] seedsUI;

    private Button[] plantButtons;

    [Header("ShopUI")]
    [SerializeField]
    private GameObject shopPanel;

    [SerializeField]
    private TextMeshProUGUI[] ShopTexts;

    [SerializeField]
    private TextMeshProUGUI[] ShopPrices;

    private InventoryManager inventory;
    private SeedsStatsManager seedsStats;

    private FarmSpot currentFarm;
    private string[] seedNames;
    private int[] prices;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<InventoryManager>();
        seedsStats = GetComponent<SeedsStatsManager>();

        seedNames = seedsStats.GetSeedNames();

        AssignButtons();
        InitializeShopItems();
        UpdateSeedsUI();
    }

    private void AssignButtons()
    {
        plantButtons = new Button[seedsUI.Length];

        for (int i = 0; i < seedsUI.Length; i++)
            plantButtons[i] = seedsUI[i].transform.GetChild(0).GetComponent<Button>();
    }

    #region Money

    public void UpdateMoneyUI(int myMoney)
    {
        moneyText.text = myMoney + "$";
    }

    #endregion

    #region Trader

    private void InitializeShopItems()
    {
        for (int i = 0; i < ShopPrices.Length; i++)
        {
            ShopPrices[i].transform.parent.GetComponent<ShopItem>().myType = (Seed.SeedType)(i % (int)Seed.SeedType.Count); // hack
        }
    }

    public void StartTrades()
    {
        Time.timeScale = 0;
        UpdateShopUI();
        UpdateShopPrices();
        shopPanel.SetActive(true);
    }

    private void UpdateShopUI()
    {
        int length = ShopTexts.Length;

        for (int i = 0; i < length; i++)
        {
            if (i < length / 2)
                ShopTexts[i].text = "Buy " + seedNames[i];
            else
                ShopTexts[i].text = "Sell " + seedNames[i - length/2];
        }
    }

    private void UpdateShopPrices()
    {
        prices = seedsStats.GetPrices();

        int length = ShopPrices.Length;

        for (int i = 0; i < length; i++)
        {
            ShopPrices[i].transform.parent.GetComponent<ShopItem>().price = prices[i];
            ShopPrices[i].text = prices[i].ToString();
        }
    }

    public void StopTrades()
    {
        Time.timeScale = 1;
        shopPanel.SetActive(false);
    }

    public void BuySeed(ShopItem data)
    {

    }

    public void SellSeed(ShopItem data)
    {

    }

    #endregion

    #region PlantUI

    public void UpdateSeedsUI()
    {
        for (int i = 0; i < inventory.myInventory.GetLength(0); i++)
        {
            seedsUI[i].text = 
                inventory.seedNames[i] + ": " + 
                inventory.myInventory[i, 0] + " | " + 
                inventory.myInventory[i, 1];
        }
    }

    public void OnClicked(int seed)
    {       
        inventory.PlantSeed(seed);
        currentFarm.PlantSeeds(seed);
        HidePlantUI();
    }

    public void ShowPlantUI(FarmSpot farm)
    {
        currentFarm = farm;
        for (int i = 0; i < plantButtons.Length; i++)
        {
            if (inventory.CanPlantSeed(i))
                plantButtons[i].gameObject.SetActive(true);
        }
    }

    public void HidePlantUI()
    {
        currentFarm = null;
        for (int i = 0; i < plantButtons.Length; i++)
        {
            if (plantButtons[i].IsActive())
                plantButtons[i].gameObject.SetActive(false);
        }
    }

    #endregion
}
