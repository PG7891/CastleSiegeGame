using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMover : MonoBehaviour
{
    public static ICollection<Soldier> selectedSoldiers;
    LayerMask soldierMask;
    LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        selectedSoldiers = new HashSet<Soldier>();
        soldierMask = LayerMask.GetMask("Soldier");
        groundMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("A");
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, soldierMask)){
                
                Soldier soldier = hit.collider.GetComponent<Soldier>();
                selectedSoldiers.Add(soldier);
                soldier.selected();
            }
            else {
                foreach(Soldier soldier in selectedSoldiers){
                    soldier.deselected();
                }
                selectedSoldiers = new HashSet<Soldier>();
            }
        }

        if (Input.GetMouseButtonDown(1)){
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask)){
                foreach(Soldier soldier in selectedSoldiers){
                    soldier.playerOrder(hit.point);
                }
            }
        }
    }
}
