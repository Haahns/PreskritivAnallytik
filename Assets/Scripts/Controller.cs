using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Controller : Agent {

    Rigidbody2D rb;
    Vector2 startingPosition;
    GameObject ball;
    float y;
    float bally;
    int direction = 0;
    public enum heuristicModes {
        AI,
        human
    }
    public Dictionary<string, UnityEngine.KeyCode> controls = new Dictionary<string, KeyCode>();
    public heuristicModes heuristicMode = new heuristicModes();
    void Update ()
    {
        y = transform.position.y;
        bally = ball.transform.position.y;
        RequestDecision();
    }

    public override void Initialize(){
        startingPosition = transform.position;
        y = startingPosition.y;
        rb = this.transform.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        // Get stuff
        
        ball = GameObject.Find("Ball");
        bally = ball.transform.position.y;
        if (transform.position.x > 0){
            controls.Add("up", KeyCode.UpArrow);
            controls.Add("down", KeyCode.DownArrow);
        }
        else {
            controls.Add("up", KeyCode.W);
            controls.Add("down", KeyCode.S);
        }
    }

    public override void OnEpisodeBegin(){
        // Reset position and speed, call Reset() from ball script
        transform.position = startingPosition;
        rb.velocity = Vector2.zero;
        ball.GetComponent<Ball_Script>().Reset();
    }
    public override void CollectObservations(VectorSensor sensor){
        // observationer av padelns och bollens positioner 
        // (eventuellt också hastighet - beror på hur mycket agenten kan använda tidigare observationer)
        sensor.AddObservation(transform.position.y);
        sensor.AddObservation(ball.transform.position.y);
        // Obserationen av bollens x-koordinat är beroende av vilken sida agenten spelar på
        if (transform.position.x > 0)
        {
            sensor.AddObservation(ball.transform.position.x);
        }
        else 
        {
            sensor.AddObservation(ball.transform.position.x * -1);
        }
        // Borde det finnas en observation för motspelaren?
    }

    public override void OnActionReceived(ActionBuffers actions){
        // TODO: Lägg kontroll hit!
        direction = 0;        
        if (actions.DiscreteActions[0] == 2) {
            direction = direction + 1;
        } 
        if (actions.DiscreteActions[0] == 0) 
        {
            direction = direction - 1;
        }
        transform.Translate(new Vector2 (0f, 6f) * Time.deltaTime * direction,Space.World);

        // TODO: Belöningar
        // Liten belöning för att träffa bollen - I OnCollisionEnter2D nedanför
        // Stor belöning för att få ett poäng och / eller vinna spelet

    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.name == "Ball"){
            var rb = col.transform.GetComponent<Rigidbody2D>();
            //if (Vector2.Angle(rb.velocity, Vector2.up) > 40 && Vector2.Angle(rb.velocity, Vector2.up) < 140){
                AddReward(0.005f);
            //}
        }
    }

    public void Score(){
        AddReward(0.09f);
    }

    public void Win(){
        AddReward(1f);
        EndEpisode();
        Debug.Log(transform.name + " " + GetCumulativeReward());
    }

    public void End(float reward = 0){
        AddReward(reward);
        EndEpisode();
        Debug.Log(transform.name + " " + GetCumulativeReward());
    }

    public override void Heuristic(in ActionBuffers actionsOut){

        // TODO: Manuell kontroll
        var discreteActionsOut = actionsOut.DiscreteActions;
        direction = 1;
        if ((int) heuristicMode  == 1) // Human player
        {              
            if (Input.GetKey (controls["up"]))
            {
                direction = direction + 1;
            } 
            if (Input.GetKey (controls["down"])) 
            {
                direction = direction - 1;
            }
        }
        else if ((int) heuristicMode  == 0) // Simple AI player
        {
            if (bally > y)
            {
                direction = direction + 1;
            } 
            if (bally < y) 
            {
                direction = direction - 1;
            }
        }
        discreteActionsOut[0] = direction;
    }
}
