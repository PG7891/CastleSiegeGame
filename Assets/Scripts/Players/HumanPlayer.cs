using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HumanPlayer : Player
{
    LayerMask selectable;
    public GameObject selected;
    public static HumanPlayer humanPlayer;

    void Start()
    {
        selectable = LayerMask.GetMask("District");
        humanPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<HumanPlayer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (selected == null)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectable))
                {
                    selected = hit.collider.gameObject;
                    selected.GetComponent<District>().selected();
                }
            }
            else if (selected.GetComponent<District>().teamNumber != 0)
            {
                selected.GetComponent<District>().deselected();
                selected = null;
            }
        }
    }
}
