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

    private InventoryManager inventory;
    private FarmSpot currentFarm;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<InventoryManager>();
        AssignButtons();
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


    #region PlantUI

    public void UpdateSeedsUI()
    {
        for (int i = 0; i < inventory.myInventory.GetLength(0); i++)
        {
            seedsUI[i].text = inventory.seedNames[i] + ": " + inventory.myInventory[i, 0];
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
