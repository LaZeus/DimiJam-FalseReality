using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    private int myMoney;

    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();
        uiManager.UpdateMoneyUI(myMoney);
    }

    public bool CanAfford(int value)
    {
        return myMoney >= value;
    }

    public void PayMoney(int value)
    {
        myMoney -= value;
        uiManager.UpdateMoneyUI(myMoney);
    }

    public void GetMoney(int value)
    {
        myMoney += value;
        uiManager.UpdateMoneyUI(myMoney);
    }
}
