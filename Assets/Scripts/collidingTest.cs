using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidingTest : MonoBehaviour
{
    void Start(){
        Debug.Log("HERERGe");
    }
    void OnCollisionEnter(Collision other) {
        Debug.Log(other.gameObject);
    }
}
