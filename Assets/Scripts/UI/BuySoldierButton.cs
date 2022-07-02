using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySoldierButton : MonoBehaviour
{
    public int soldierNumber;

    // Update is called once per frame
    public void onClick(){
        HumanPlayer.humanPlayer.buySoldier(HumanPlayer.humanPlayer.selected.GetComponent<District>(), soldierNumber);
    }
}
