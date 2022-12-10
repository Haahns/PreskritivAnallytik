
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball_Script : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject GameManager;
    Vector2 startposition = Vector2.zero;
    Vector2 startspeed;
    float random;
    int[] sides = {-1,1};
    int sideChoice;
    int[] score = {0,0};

    float limitangle = Mathf.PI/2 * 0.1f;

    TMP_Text ScoreDisplay;
    void Start()
    {
        GameManager = GameObject.Find("GameManager");

        score[0] = 0;
        score[1] = 0;

        transform.position = startposition;

        sideChoice = sides[Random.Range(0,2)];

        random = Random.Range(limitangle, Mathf.PI/2 - limitangle) * sideChoice;
        startspeed = new Vector2(Mathf.Sin(random) * 250, Mathf.Cos(random) * 250);

        Debug.Log(random);

        rb = this.transform.GetComponent<Rigidbody2D>();
        rb.AddForce(startspeed);
        
        ScoreDisplay = Camera.main.GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {

    }

    void reset()
    {
        sideChoice = sideChoice * -1;
        transform.position = startposition;
        random = Random.Range(limitangle, Mathf.PI/2 - limitangle) * sideChoice;

        startspeed = new Vector2(Mathf.Sin(random) * 250, Mathf.Cos(random) * 250);
        rb.AddForce(startspeed);
        rb.velocity = Vector2.zero;
    }

        void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Wall right")
        {
            score[1] += 1;
            //Debug.Log(score[0].ToString() + " - " + score[1].ToString());
            //GameManager.GetComponentInChildren(TextMesh)[0].text = score[0].ToString() + " - " + score[1].ToString();
            ScoreDisplay.text = score[0].ToString() + " - " + score[1].ToString();
            reset();
        }
        else if (col.gameObject.name == "Wall left")
        {
            score[0] += 1;
            //Debug.Log(score[0].ToString() + " - " + score[1].ToString());
            //Camera.main.GetComponentInChildren<TextMeshPro>();
            //GameManager.transform.GetChild(0).GetComponent(TextMesh) = score[0].ToString() + " - " + score[1].ToString();
            ScoreDisplay.text = score[0].ToString() + " - " + score[1].ToString();
            reset();
        }
        else Debug.Log(col.gameObject.name);
    }
}
