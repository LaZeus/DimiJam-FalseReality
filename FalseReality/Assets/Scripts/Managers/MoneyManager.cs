using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    private int myMoney;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetMoney(int value)
    {
        myMoney += value;
    }

    public bool CanAfford(int value)
    {
        return myMoney >= value;
    }

    public void PayMoney(int value)
    {
        myMoney -= value;
    }
}
