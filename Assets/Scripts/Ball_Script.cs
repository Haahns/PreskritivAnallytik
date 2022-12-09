
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Script : MonoBehaviour
{
    Vector2 startposition = Vector2.zero;
    Vector2 startspeed;
    float random;
    // Start is called before the first frame update
    void Start()
    {
        random = 25f;
        startspeed = new Vector2(Mathf.Cos(random)/50, Mathf.Sin(random)/50);
        transform.position = startposition;
        Rigidbody2D rigibody = this.transform.GetComponent<Rigidbody2D>();
        rigibody.AddForce(startspeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.transform.GetComponent<Rigidbody2D>());
    }

    void reset(){
        transform.position = startposition;
        random = Random.Range(0f,260f);
        startspeed = new Vector2(Mathf.Cos(random)/50, Mathf.Sin(random)/50);
        Rigidbody2D rigibody = this.transform.GetComponent<Rigidbody2D>();
        rigibody.AddForce(startspeed);

    }
}
