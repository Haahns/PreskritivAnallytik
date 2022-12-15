
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball_Script : MonoBehaviour
{
    Rigidbody2D rb;
    // GameObject GameManager;
    GameObject left;
    GameObject right;
    Vector2 startposition = Vector2.zero;
    Vector2 startspeed;
    float random;
    float[] sides = {0,Mathf.PI};
    float sideChoice;
    public int[] score = {0,0};

    float limitangle = Mathf.PI * 0.4f;

    int bounceCount = 0;

    [Range(1,20)]
    public int pointsToWin = 1;

    [Range(1,100)]
    public int bouncesToTimeout = 50;

    string leftName = "Left";
    string rightName = "Right";

    TMP_Text ScoreDisplay;
    void Start()
    {
        // GameManager = GameObject.Find("GameManager");

        left = GameObject.Find("PaddleLeft");
        right = GameObject.Find("PaddleRight");

        score[0] = 0;
        score[1] = 0;

        transform.position = startposition;

        sideChoice = sides[Random.Range(0,2)];
        
        random = Random.Range(limitangle, Mathf.PI/2 - limitangle) + sideChoice;
        startspeed = new Vector2(Mathf.Cos(random) * 1000, Mathf.Sin(random) * 1000);
        // Debug.Log(random);
        rb = this.transform.GetComponent<Rigidbody2D>();
        rb.AddForce(startspeed);
        
        ScoreDisplay = Camera.main.GetComponentInChildren<TMP_Text>();

    }

    void Update()
    {
        if (bounceCount == bouncesToTimeout){
            ScoreDisplay.text = score[0].ToString() + " - " + score[1].ToString() + "\n TIMEOUT";
        }
        // Debug.Log(Vector2.Angle(rb.velocity, Vector2.up));
    }

    public void Reset()
    {
        ScoreDisplay.text = score[0].ToString() + " - " + score[1].ToString();

        bounceCount = 0;
        sideChoice = sideChoice + Mathf.PI;
        transform.position = startposition;
        rb.velocity = Vector2.zero;

        random = Random.Range(limitangle, Mathf.PI/2 - limitangle) + sideChoice;
        startspeed = new Vector2(Mathf.Cos(random) * 1000, Mathf.Sin(random) * 1000);
        rb.AddForce(startspeed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Wall right")
        {
            score[0] += 1;
            //Debug.Log(score[0].ToString() + " - " + score[1].ToString());
            //GameManager.GetComponentInChildren(TextMesh)[0].text = score[0].ToString() + " - " + score[1].ToString();
            if (score[0] >= pointsToWin){
                left.GetComponent<Controller>().Win();
                right.GetComponent<Controller>().End(-0.09f);
                ScoreDisplay.text = leftName + " wins!";
                Debug.Log(leftName + " wins!");
                score[0] = 0;
                score[1] = 0;
            }
            else 
            {
                ScoreDisplay.text = score[0].ToString() + " - " + score[1].ToString();
                left.GetComponent<Controller>().Score();
                left.GetComponent<Controller>().End();
            }
            Reset();
        }
        else if (col.gameObject.name == "Wall left")
        {
            score[1] += 1;
            //Debug.Log(score[0].ToString() + " - " + score[1].ToString());
            //Camera.main.GetComponentInChildren<TextMeshPro>();
            //GameManager.transform.GetChild(0).GetComponent(TextMesh) = score[0].ToString() + " - " + score[1].ToString();
            if (score[1] >= pointsToWin){
                right.GetComponent<Controller>().Win();
                left.GetComponent<Controller>().End(-0.09f);
                ScoreDisplay.text = rightName + " wins!";
                Debug.Log(rightName + " wins!");
                score[0] = 0;
                score[1] = 0;
            }
            else 
            {
                ScoreDisplay.text = score[0].ToString() + " - " + score[1].ToString();
                right.GetComponent<Controller>().Score();
                right.GetComponent<Controller>().End();
            }
            Reset();
        }
        /*
        else if (col.gameObject == left){
            Debug.Log("End Right");
            right.GetComponent<Controller>().End();
        }
        else if (col.gameObject == right){
            // Debug.Log("End Left");
            left.GetComponent<Controller>().End();
        }
        */
        if (bounceCount > bouncesToTimeout){
            Reset();
            if (pointsToWin == 1){
                bounceCount = 0;
                left.GetComponent<Controller>().End();
                right.GetComponent<Controller>().End();
                Reset();
            }
        }
        bounceCount += 1;
        if (bounceCount == bouncesToTimeout)
        {
            Debug.Log("TIMEOUT");
        }
    }
}
