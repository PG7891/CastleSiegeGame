using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int teamNumber;
    public Capital capital;
    public float money = 1000;
    float moneyEarned = 10;
    float moneyEarnInterval = 5;
    
    void Awake()
    {
        InvokeRepeating("passiveMoney", moneyEarnInterval, moneyEarnInterval);
    }
    public void earnMoney(float money)
    {
        this.money += money;
    }

    public void buySoldier(District district, int soldierNumber)
    {
        if (district.teamNumber != teamNumber)
        {
            Debug.Log("buying soldier for wrong team");
        }
        district.shop.buySoldier(soldierNumber);
    }

    public void passiveMoney()
    {
        earnMoney(moneyEarned);
    }
}
