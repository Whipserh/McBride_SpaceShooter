using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CircleExercise : MonoBehaviour
{
    public Vector3 position;
    private int index = 0;
    public float radius = 1;
    private List<float> angles;



    void Start()
    {
        
        position = Vector3.zero;


        GenerateAngles();
    }


    public void GenerateAngles()
    {
        angles = new List<float>();
        //generate 10 angles from 0 to 360 and add then to a list
        for (int i = 0; i < 10; i++)
        {
            angles.Add(Random.Range(0, 360));
        }
        timer = 0;
    }


    private float timer;
    void Update()
    {

        
        timer += Time.deltaTime;
        //every 6 seconds we create a new batch of angles
        if (timer > 6)
        {
            GenerateAngles();
        }




        //when the player hits space bar change the index that we are at
        if (Input.GetKeyDown(KeyCode.Space))
        {
            index = (index + 1) % angles.Count;
        }

        //draw the vector at the index of the list
        Debug.DrawLine(position, position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * angles[index]), Mathf.Sin(Mathf.Deg2Rad * angles[index]))) * radius);
    }


    
}
