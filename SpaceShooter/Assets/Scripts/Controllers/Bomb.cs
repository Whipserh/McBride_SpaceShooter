using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float followRadius, speed, detectDetinationRadius;
    public Transform Enemy;
    public float updateTimer;
    void Start()
    {
        goalPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        DrawDetectionCirle(5);
        UpdateMove();

        //explode if enemy is too close
        if ((Enemy.position - transform.position).magnitude <= detectDetinationRadius)
        {
            Destroy(Enemy);
            Destroy(transform);
        }
    }

    public void DrawDetectionCirle(int circlePoints)
    {
        //dummy proof the game designer
        if (circlePoints < 3)
        {
            circlePoints = 3;
        }



        //check to see if the enemy is too close to the player
        Color detection;
        if ((Enemy.position - transform.position).magnitude >= followRadius)
        {
            detection = Color.white;
        }
        else
        {
            detection = Color.red;
        }

        List<float> angles = new List<float>();
        //create the points
        for (int i = 0; i < circlePoints; i++)
        {
            angles.Add(i * 360 / circlePoints);
        }

        //draw the circle
        for (int i = 0; i < circlePoints - 1; i++)
        {
            Debug.DrawLine(transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[i]), Mathf.Sin(Mathf.Deg2Rad * angles[i]))) * followRadius, transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[i + 1]), Mathf.Sin(Mathf.Deg2Rad * angles[i + 1]))) * followRadius, detection);
        }

        Debug.DrawLine(transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[circlePoints - 1]), Mathf.Sin(Mathf.Deg2Rad * angles[circlePoints - 1]))) * followRadius, transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[0]), Mathf.Sin(Mathf.Deg2Rad * angles[0]))) * followRadius, detection);

    }

    Vector3 goalPosition, oldVelocity;
    float updateMovementTimer = 0;
    public void UpdateMove()
    {
        Vector3 velocity;
        Vector3 direction = Enemy.position - transform.position;
        //if the enemy player is too close to the bomb it will slowly chase them
        if (direction.magnitude > followRadius)
        {
            return;
        }

        //we know that enemy is close enough to the bomb so the bomb slowly follows
       
        //find the direction to the player from where the enemy is currently at
        direction = goalPosition - transform.position;
        direction.Normalize();

        // lerp the velocity
        velocity = Vector3.Lerp(oldVelocity, direction * speed, updateMovementTimer);


        //move towards that location at velocity
        transform.position += velocity * Time.deltaTime;

        updateMovementTimer += Time.deltaTime;



        //after a certain amount of time go to where the player is now
        if (updateMovementTimer >= updateTimer)
        {
            Debug.Log("update path");

            //get the direction where the enemy is going
            direction = Enemy.position - transform.position;
            //get the max distance that we are traveling
            float maxTravel = direction.magnitude;
            //normalize 
            direction.Normalize();


            //choose a new location
            goalPosition = transform.position + direction * Random.Range(maxTravel / 2, maxTravel);
            updateMovementTimer = 0;


            oldVelocity = velocity;
        }

    }

}
