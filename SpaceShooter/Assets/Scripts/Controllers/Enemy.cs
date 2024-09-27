using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using PlasticGui.Kqueue;

public class Enemy : MonoBehaviour
{

    public Transform playerTransform;
    public float moveSpeed = 2;
    public float timerChange = 2f;
    private float updateMovementTimer = 0f;
    private Vector3 goalPosition;
    private void Start()
    {
        updateMovementTimer = timerChange;
        goalPosition = transform.position;

    }

    private void Update()
    {
        EnemyMovement();
    }

    public void EnemyMovement()
    {


        Vector3 direction;

        //after a certain amount of time go to where the player is now
        if (updateMovementTimer >= timerChange)
        {
            Debug.Log("update path");
            
            //get the direction where the enemy is going
            direction = playerTransform.position - transform.position;
            //get the max distance that we are traveling
            float maxTravel = direction.magnitude;
            //normalize 
            direction.Normalize();


            //choose a new location
            goalPosition = transform.position + direction*Random.Range(maxTravel/2, maxTravel);
            updateMovementTimer = 0;
            Debug.Log(transform.position);
            Debug.Log(goalPosition);
        }


        //find the direction to the player from where the enemy is currently at
        direction = goalPosition - transform.position;
        direction.Normalize();

        //move towards that location
        transform.position += direction * moveSpeed * Time.deltaTime;
        
        updateMovementTimer+= Time.deltaTime; 

    }
}
