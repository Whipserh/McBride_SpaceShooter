using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        targetAngle = Random.Range(0, 180);        
    }

    public Transform target;
    public float targetAngle, bufferAngle = .5f;
    public float angleSpeed = 45;
    // Update is called once per frame
    void Update()
    {

        exercise2();

    }

    public void exercise1()
    {
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);

        if (Mathf.Abs(transform.eulerAngles.z % 360 - (targetAngle) % 360) > bufferAngle)
        {
            transform.Rotate(new Vector3(0, 0, angleSpeed * Time.deltaTime));
        }
    }

    public void exercise2()
    {
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);
        Debug.DrawLine(transform.position, target.position, Color.blue);
        Vector3 direction = target.position - transform.position;

        
       

        float currentAngle = (Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg);
        float targetAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        //Debug.Log(currentAngle + ", " + targetAngle);
        if (currentAngle < 0)
            currentAngle += 360;
        if (targetAngle < 0)
            targetAngle += 360;
        Debug.Log(currentAngle + ", " + targetAngle);
        //Debug.Log(Mathf.FloorToInt(180 + currentAngle) % 360);
        //if we are not in our buffer range then we move
        if (Mathf.Abs(currentAngle - targetAngle) > 0)
        {

            if (targetAngle < currentAngle)
            {
                if(Mathf.Abs(currentAngle - targetAngle) < 180)
                {
                    transform.Rotate(new Vector3(0, 0, -1 * angleSpeed * Time.deltaTime));
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, angleSpeed * Time.deltaTime));//turn counterclockwise
                }
            }
            else
            {
                if (Mathf.Abs(currentAngle - targetAngle) < 180)
                {
                    transform.Rotate(new Vector3(0, 0, angleSpeed * Time.deltaTime));//turn counterclockwise
                    
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, -1 * angleSpeed * Time.deltaTime));
                }

            }


        }

    }//end exorcise

    

}
