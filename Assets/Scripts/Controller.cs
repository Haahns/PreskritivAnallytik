using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

int direction = 0;
    void Update ()
    {
        direction = 0;
        if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
            direction = direction + 1;
        } 
        if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S) ) 
        {
            direction = direction - 1;
        }
        this.transform.Translate(new Vector2 (0f, 4f) * Time.deltaTime * direction,Space.World);
    }
}