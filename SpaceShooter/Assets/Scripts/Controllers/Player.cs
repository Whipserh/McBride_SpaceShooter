using Codice.Client.BaseCommands.CheckIn.Progress;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject PowerPrefab;
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;
    public float speed = 0.001f, accleration = 1f;

    public float targetSpeed = 3;
    public float timeToTargetSpeed = 2f;


    private float deceleration = 0f;
    public float decelerationTime = 3f;

    private void Start()
    {
        SpawnPowerups(4, 6);

        accleration = targetSpeed / timeToTargetSpeed;

        List<string> words = new List<string>();
        words.Add("Dog");
        words.Add("Cat");
        words.Add("Log");
        words.Insert(1, "Rat");

        Debug.Log("The cat is at index: " +words.IndexOf("Cat"));
    }



    private Vector3 velocity;// = Vector3.right * 0.001f;
    void Update()
    {
        
        PlayerMovement();
        EnemyRadar(1, 7);

        //spawn a bomb
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnSemiCircleBombs(1.5f, 1);
        }

    }


    public void EnemyRadar(float radius, int circlePoints)
    {

        //dummy proof the game designer
        if (circlePoints < 3)
        {
            circlePoints = 3;
        }



        //check to see if the enemy is too close to the player
        Color detection;
        if((enemyTransform.position - transform.position).magnitude > radius)
        {
            detection = Color.green;
        }
        else
        {
            detection = Color.red;
        }

        List<float> angles = new List<float>();
        //create the points
        for(int i = 0; i < circlePoints; i++)
        {
            angles.Add(i * 360 /circlePoints);
        }

        //draw the circle
        for(int i = 0; i < circlePoints - 1; i++)
        {
            Debug.DrawLine(transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[i]), Mathf.Sin(Mathf.Deg2Rad * angles[i])))*radius, transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[i+1]), Mathf.Sin(Mathf.Deg2Rad * angles[i+1]))) * radius, detection);
        }

        Debug.DrawLine(transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[circlePoints - 1]), Mathf.Sin(Mathf.Deg2Rad * angles[circlePoints - 1]))) * radius, transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[0]), Mathf.Sin(Mathf.Deg2Rad * angles[0]))) * radius, detection);



    }

    public Transform powerParentTransform;
    public void SpawnPowerups(float radius, int numberOfPowerups)
    {

        List<float> angles = new List<float>();
        //create the points
        for (int i = 0; i < numberOfPowerups; i++)
        {
            float angle = (i * 360 / numberOfPowerups);
            Instantiate(PowerPrefab, transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle))) * radius, transform.rotation, powerParentTransform);
        }

    }

    public void SpawnSemiCircleBombs(float radius, float numOfBombs)
    {
        //we want the bombs to be right behhind the player, we don't want bombs to spawn on the right and left side so our
        //angle can't be 0 nor can it be pi which means we need to break  up the 180 degrees into numberOfBombs + 2
        //(the beginning and the end)

        for(int i = 1; i <= numOfBombs; i++)
        { 
            float angle = (i)*180/(numOfBombs + 1);
            Vector3 bombPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * radius;
            GameObject tempBomb = Instantiate(bombPrefab, transform.position - bombPosition, transform.rotation, bombsTransform);
            tempBomb.GetComponent<Bomb>().Enemy = enemyTransform;
        }
    }



    public void PlayerMovement()
    {
        //reset the volicity at the start of every frame
        //NOTE: if we remove this we now have drag/momentum in our game
        //velocity = Vector3.zero;


        //NOTE: this is updating the player's velcoity based on the players acceleration
        Vector3 newVelocity = velocity;
                
        //hold left key
        if (Input.GetKey(KeyCode.LeftArrow)){
            newVelocity += Vector3.left * accleration * Time.deltaTime;

        //hold down right velocity
        } 
        if (Input.GetKey(KeyCode.RightArrow)){
            newVelocity += Vector3.right * accleration * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.UpArrow)){
            newVelocity += Vector3.up * accleration * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.DownArrow)){
            newVelocity += Vector3.down * accleration * Time.deltaTime;
        }

        //if we are not pressing a button to move
        if (newVelocity != velocity)
        {
            if (newVelocity.magnitude <= targetSpeed)
            {
                velocity = newVelocity;
            }
            deceleration = 0;
        }
        else // velocity is the same and we didn't move
        {
            newVelocity = Vector3.Lerp(velocity, Vector3.zero, deceleration/decelerationTime);
            deceleration += Time.deltaTime;
            if(newVelocity == Vector3.zero)
            {
                velocity = Vector3.zero;
            }
        }
        
        

        //NOTE: this is updating the player's position based on its speed
        //velocity.Normalize();
        //velocity *= speed;
        transform.position += newVelocity * Time.deltaTime;


        //we are pressing a button to move 
        

    }



}
