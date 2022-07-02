using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    District district;
    static public GameObject[] soldierPrefabs;
    static public float[] prices;
    GameObject shopUI = ShopUI.ui;
    public Shop(District district)
    {
        this.district = district;
    }

    public void buySoldier(int soldierNumber)
    {
        if (playerMoney < prices[soldierNumber])
        {
            Debug.Log("not enough money");
        }
        else
        {
            playerMoney -= prices[soldierNumber];
            SoldierSpawner.spawnSoldier(district, soldierNumber);
            Debug.Log("Team " + district.teamNumber + " has " + district.capital.player.money);
        }
    }

    public void openShop(bool b){
        shopUI.SetActive(b);
    }

    public float playerMoney { get => district.capital.player.money; set { district.capital.player.money = value; } }
}
