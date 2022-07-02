using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDebugging : MonoBehaviour
{
    /*
Writen by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.  
Simple flycam I made, since I couldn't find any others made public.  
Made simple to use (drag and drop, done) for regular keyboard layout  
wasd : basic movement
shift : Makes camera accelerate
space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/
 
 
float mainSpeed = 20.0f; //regular speed
float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
float maxShift= 1000.0f; //Maximum speed when holdin gshift
float camSens = 0.5f; //How sensitive it with mouse
private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
private float totalRun  = 1.0f;
private float yaw = 0.0f;
private float pitch = 0.0f;

void Update () {
    Cursor.lockState = CursorLockMode.Locked;
    /*lastMouse = Input.mousePosition - lastMouse ;
    lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
    lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
    transform.eulerAngles = lastMouse;
    lastMouse =  Input.mousePosition;*/

    yaw += camSens * Input.GetAxis("Mouse X");
    pitch -= camSens * Input.GetAxis("Mouse Y");

    transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

    //Mouse  camera angle done.  
   
    //Keyboard commands
    float f = 0.0f;
    var p = GetBaseInput();
    if (Input.GetKey (KeyCode.LeftShift)){
        totalRun += Time.deltaTime;
        p  = p * totalRun * shiftAdd;
        p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
        p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
        p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
    }
    else{
        totalRun = Mathf.Clamp(totalRun * 0.5f, 1, 1000);
        p = p * mainSpeed;
    }
   
    p = p * Time.deltaTime;
    if (Input.GetKey(KeyCode.Space)){ //If player wants to move on X and Z axis only
        f = transform.position.y;
        transform.Translate(p);
        transform.position = new Vector3(transform.position.x, f, transform.position.z);
    }
    else{
        transform.Translate( p);
    }
   
}
 
private Vector3 GetBaseInput(){ //returns the basic values, if it's 0 than it's not active.
    Vector3 p_Velocity = Vector3.zero;
    if (Input.GetKey (KeyCode.W)){
        p_Velocity += new Vector3(0, 0 , 1);
    }
    if (Input.GetKey (KeyCode.S)){
        p_Velocity += new Vector3(0, 0 , -1);
    }
    if (Input.GetKey (KeyCode.A)){
        p_Velocity += new Vector3(-1, 0 , 0);
    }
    if (Input.GetKey (KeyCode.D)){
        p_Velocity += new Vector3(1, 0 , 0);
    }
    return p_Velocity;
}
}
