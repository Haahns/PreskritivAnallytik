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

    int direction = 0;
    void Update ()
    {
        RequestDecision();

        // TODO: Move player controls to Heuristic
    }

    public override void Initialize(){
        startingPosition = transform.position;
        rb = this.transform.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        // Get stuff
        
        ball = GameObject.Find("Ball");
    }

    public override void OnEpisodeBegin(){
        // Reset position and speed, call Reset() from ball script
        transform.position = startingPosition;
        rb.velocity = Vector2.zero;
        ball.GetComponent<Ball_Script>().Reset();
    }
    public override void CollectObservations(VectorSensor sensor){
        // VectorSensor.AddObservation(IList<float> observation)

        // TODO: Lägg till observationer om padelns och bollens positioner 
        // (eventuellt också hastighet - beror på hur mycket agenten kan använda tidigare observationer)
        sensor.AddObservation(transform.position.y);
        sensor.AddObservation(ball.transform.position.y);
        // TODO: Gör obserationen av bollens x-koordinat beroende av vilken sida agenten spelar på
        if (transform.position.y > 0){
            sensor.AddObservation(ball.transform.position.x);
            }
        else {
            sensor.AddObservation(ball.transform.position.x * -1);
            }
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
        transform.Translate(new Vector2 (0f, 4f) * Time.deltaTime * direction,Space.World);

        // TODO: Belöningar

    }

    public override void Heuristic(in ActionBuffers actionsOut){

        // TODO: Manuell kontroll
        var discreteActionsOut = actionsOut.DiscreteActions;
        direction = 1;
        if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
            direction = direction + 1;
        } 
        if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S) ) 
        {
            direction = direction - 1;
        }
        discreteActionsOut[0] = direction;
    }
}
