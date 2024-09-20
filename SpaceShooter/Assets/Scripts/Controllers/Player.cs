using Codice.Client.BaseCommands.CheckIn.Progress;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;
    public float speed = 0.001f, accleration = 1f;

    public float targetSpeed = 3;
    public float timeToTargetSpeed = 2f;


    private void Start()
    {
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

        if(newVelocity.magnitude <= targetSpeed)
        {
            velocity = newVelocity;
        }

        //NOTE: this is updating the player's position based on its speed
        //velocity.Normalize();
        //velocity *= speed;
        transform.position += velocity * Time.deltaTime;
        
    }

    

}
