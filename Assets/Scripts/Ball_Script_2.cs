using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Script_2 : MonoBehaviour
{
Rigidbody2D rb;
Vector2 speed;
    // Start is called before the first frame update

    void Awake(){
       // rb = gameObject.AddComponent<Rigidbody2D>();
    }
    void Start()
    {
    //give it the direction you want as before;
rb = GetComponent<Rigidbody2D>();
speed  = new Vector2(5f,5f);
rb.velocity = speed;
}
 
void Update(){
 
}
 
}
