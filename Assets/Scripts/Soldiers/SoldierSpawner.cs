using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    static GameObject[] soldierPrefabs = MapGen.soldierPrefabs;
    //static public LayerMask soldierLayerMask;
    void Start(){
        //soldierLayerMask = LayerMask.GetMask("Soldier");
    }
    static public void spawnSoldier(District district, int soldierNumber){
        Soldier soldier = Instantiate(soldierPrefabs[soldierNumber], district.soldierSpawnPoint, Quaternion.identity).GetComponent<Soldier>();
        soldier.district = district;
        soldier.setSoldierColor(Initializer.colors[district.teamNumber]);
    }

    
}
