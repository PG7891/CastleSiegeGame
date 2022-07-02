using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitUIButton : MonoBehaviour
{
    public void onClick(){
        HumanPlayer.humanPlayer.selected.GetComponent<District>().deselected();
        HumanPlayer.humanPlayer.selected = null;
    }
}
