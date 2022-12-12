
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
    int[] sides = {-1,1};
    int sideChoice;
    public int[] score = {0,0};

    float limitangle = Mathf.PI/2 * 0.2f;

    int bounceCount = 0;

    [Range(30,110)]
    public int pointsToWin = 1;

    [Range(1,10)]
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
        
        random = Random.Range(limitangle, Mathf.PI/2 - limitangle) * sideChoice;
        startspeed = new Vector2(Mathf.Sin(random) * 250, Mathf.Cos(random) * 250);
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
    }

    public void Reset()
    {
        ScoreDisplay.text = score[0].ToString() + " - " + score[1].ToString();

        bounceCount = 0;
        sideChoice = sideChoice * -1;
        transform.position = startposition;
        rb.velocity = Vector2.zero;

        random = Random.Range(limitangle, Mathf.PI/2 - limitangle) * sideChoice;
        startspeed = new Vector2(Mathf.Sin(random) * 250, Mathf.Cos(random) * 250);
        rb.AddForce(startspeed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Wall right")
        {
            score[1] += 1;
            //Debug.Log(score[0].ToString() + " - " + score[1].ToString());
            //GameManager.GetComponentInChildren(TextMesh)[0].text = score[0].ToString() + " - " + score[1].ToString();
            if (score[1] >= pointsToWin){
                left.GetComponent<Controller>().Win();
                right.GetComponent<Controller>().End();
                ScoreDisplay.text = leftName + " wins!";
                Debug.Log(leftName + " wins!");
                score[0] = 0;
                score[1] = 0;
            }
            else 
            {
                ScoreDisplay.text = score[0].ToString() + " - " + score[1].ToString();
                left.GetComponent<Controller>().Score();
            }
            Reset();
        }
        else if (col.gameObject.name == "Wall left")
        {
            score[0] += 1;
            //Debug.Log(score[0].ToString() + " - " + score[1].ToString());
            //Camera.main.GetComponentInChildren<TextMeshPro>();
            //GameManager.transform.GetChild(0).GetComponent(TextMesh) = score[0].ToString() + " - " + score[1].ToString();
            if (score[0] >= pointsToWin){
                right.GetComponent<Controller>().Win();
                left.GetComponent<Controller>().End();
                ScoreDisplay.text = rightName + " wins!";
                Debug.Log(rightName + " wins!");
                score[0] = 0;
                score[1] = 0;
            }
            else 
            {
                ScoreDisplay.text = score[0].ToString() + " - " + score[1].ToString();
                right.GetComponent<Controller>().Score();
            }
            Reset();
        }
        if (bounceCount > bouncesToTimeout){
            Reset();
            if (pointsToWin == 1){
                left.GetComponent<Controller>().End();
                right.GetComponent<Controller>().End();
            }
        }
        bounceCount += 1;
        if (bounceCount == bouncesToTimeout)
        {
            Debug.Log("TIMEOUT");
        }
    }
}
