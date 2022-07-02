using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District : MonoBehaviour
{
    int districtLevel = 1;
    public Vector3 soldierSpawnPoint;
    public Capital capital;
    public Shop shop;
    Renderer rend;
    Color color;
    public DistrictSoldier districtSoldier;
    // Start is called before the first frame update
    void Start()
    {
        districtSoldier = GetComponent<DistrictSoldier>();
        rend = GetComponent<Renderer>();
        color = rend.material.color;
        shop = new Shop(this);
        setSoldierSpawnPoint();
    }

    virtual public void setSoldierSpawnPoint()
    {
        Vector3 capToDistrict = transform.position;
        soldierSpawnPoint = capToDistrict - (5 * Vector3.Normalize(capToDistrict));
    }


    public void spawnSoldier(int soldierNumber)
    {
        shop.buySoldier(soldierNumber);
    }

    public void selected()
    {
        rend.material.color = rend.material.color * .4f;
        if (teamNumber == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            PlayerController.locked = true;
            shop.openShop(true);
        }
    }

    public void deselected()
    {
        rend.material.color = color;
        if (teamNumber == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            PlayerController.locked = false;
            shop.openShop(false);
        }
    }

    public void districtSwapTeam(int teamNumber){
        capital = Initializer.capitals[teamNumber];
        updateColor();
    }

    void updateColor(){
        color = Initializer.colors[teamNumber];
        rend.material.color = color;
    }



    public int teamNumber { get => capital.player.teamNumber; set { capital.player.teamNumber = value; } }
}
