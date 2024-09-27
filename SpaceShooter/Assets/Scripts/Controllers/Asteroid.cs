using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;
    private Vector3 goalPosition;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 1;
        arrivalDistance = 0.5f;
        maxFloatDistance = 3;
        goalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    public void AsteroidMovement()
    {
        
        //if we are within the distance of the goal then chose a new position
        if ((transform.position - goalPosition).magnitude <=arrivalDistance)
        {
            //choose a new location
            goalPosition = new Vector3(transform.position.x + Random.Range(-maxFloatDistance, maxFloatDistance), transform.position.y + Random.Range(-maxFloatDistance, maxFloatDistance));
        }


        //find the direction to the nearest asteriod
        Vector3 direction = goalPosition - transform.position;
        direction.Normalize();

        //move towards that location
        transform.position += direction*moveSpeed*Time.deltaTime;
    }
}
