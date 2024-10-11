using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtilities : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        redLength = 1;
        blueLength = 1;
    }




    public float redAngle, blueAngle, redLength, blueLength;
    // Update is called once per frame
    void Update()
    {

        Vector3 red,blue;
        red = AngleToVector(redAngle, redLength);
        blue = AngleToVector(blueAngle, blueLength);

        Debug.DrawLine(Vector3.zero, red, Color.red);
        Debug.DrawLine(Vector3.zero, blue, Color.blue);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Dot Product: " +Vector3.Dot(red, blue));
        }

    }


    public static Vector3 AngleToVector(float angle, float length)
    {
        return (new Vector3(Mathf.Cos(Mathf.Deg2Rad* angle), Mathf.Sin(Mathf.Deg2Rad* angle))) * length;
    }



}
