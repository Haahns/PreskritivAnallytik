using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

void Update ()
{
if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
this.transform.Translate(new Vector2 (0f, 4f) * Time.deltaTime,Space.World);
} else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S) ) {
this.transform.Translate(new Vector2 (0f, -4f) * Time.deltaTime,Space.World);
} 
}
}