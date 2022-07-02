using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capital : District
{
    public District[] districts;
    public Player player;
    

    override public void setSoldierSpawnPoint(){
        Vector3 centerToCap = transform.position - MapGen.center;
        soldierSpawnPoint = centerToCap - Vector3.Normalize(centerToCap);
    }
}
