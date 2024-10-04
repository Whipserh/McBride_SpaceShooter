using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float followRadius, maxSpeed, detectDetinationRadius;
    public Transform Enemy;
    public float updateTimer;
    void Start()
    {
        goalPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        DrawDetectionCirle(11);
        UpdateMove();

        //explode if enemy is too close
        if ((Enemy.position - transform.position).magnitude <= detectDetinationRadius)
        {
            Destroy(Enemy.gameObject);
            Destroy(gameObject);
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
            detection = Color.yellow;
        }


        //create the points
        List<float> angles = new List<float>();
        for (int i = 0; i < circlePoints; i++)
        {
            angles.Add(i * 360 / circlePoints);
        }

        //draw the follow circle
        for (int i = 0; i < circlePoints - 1; i++)
        {
            Debug.DrawLine(transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[i]), Mathf.Sin(Mathf.Deg2Rad * angles[i]))) * followRadius, transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[i + 1]), Mathf.Sin(Mathf.Deg2Rad * angles[i + 1]))) * followRadius, detection);
        }

        Debug.DrawLine(transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[circlePoints - 1]), Mathf.Sin(Mathf.Deg2Rad * angles[circlePoints - 1]))) * followRadius, transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[0]), Mathf.Sin(Mathf.Deg2Rad * angles[0]))) * followRadius, detection);


        //draw the follow circle
        for (int i = 0; i < circlePoints - 1; i++)
        {
            Debug.DrawLine(transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[i]), Mathf.Sin(Mathf.Deg2Rad * angles[i]))) * detectDetinationRadius, transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[i + 1]), Mathf.Sin(Mathf.Deg2Rad * angles[i + 1]))) * detectDetinationRadius, Color.red);
        }

        Debug.DrawLine(transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[circlePoints - 1]), Mathf.Sin(Mathf.Deg2Rad * angles[circlePoints - 1]))) * detectDetinationRadius, transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[0]), Mathf.Sin(Mathf.Deg2Rad * angles[0]))) * detectDetinationRadius, Color.red);

        //**** i know that i can save some squeeze time by combining these 2 loops into 1 but for read ability i'm giong to keep it seperate.
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
            updateMovementTimer = updateTimer;
            oldVelocity = Vector3.zero;
            return;
        }



        //---------------------------------------------------------------------------------------------------------------
        //we know that enemy is close enough to the bomb so the bomb slowly follows
       
        
        //find the direction to the player from where the enemy is currently at
        direction = goalPosition - transform.position;
        direction.Normalize();


        // lerp the velocity
        velocity = Vector3.Lerp(oldVelocity, direction * maxSpeed, updateMovementTimer);



        //move towards that location at velocity and update the timer
        transform.position += velocity * Time.deltaTime; //distance travelled
        updateMovementTimer += Time.deltaTime; //time elapsed

        //after a certain amount of time go to where the player is now
        if (updateMovementTimer > updateTimer)
        {
            //choose a new location
            goalPosition = Enemy.position;
            updateMovementTimer = 0;
            oldVelocity = velocity;
        }

    }
    public float acceleration = 2;
}
