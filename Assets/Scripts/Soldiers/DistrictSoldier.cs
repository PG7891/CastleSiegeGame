using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictSoldier : Soldier
{
    void Start(){
        health = 100;
        district = GetComponent<District>();
        col = GetComponent<Collider>();
    }

    override protected void getAttacked(Soldier enemy)
    {
        takeDamage(enemy.damage, false);
        if (checkDead())
        {
            enemy.killedDistrict(district);
        }
    }

}
