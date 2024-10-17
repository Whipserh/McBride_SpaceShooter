using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : MonoBehaviour
{


    public float angleSpeed;
    public float MissileSpeed;
    public float detinationRadius;
    private Transform target;
    public float followRadius;

    // Update is called once per frame
    void Update()
    {
        target = GrabClosestCollider();
        if(target != null && Vector3.Distance(target.position, transform.position) < detinationRadius)
        {
            //Debug.Log("Destory everything");
            Destroy(target.gameObject);
            Destroy(transform.gameObject);
        }
        UpdateMissileMovement();
        
    }

    public void UpdateMissileMovement()
    {
        if(target != null)//there is something nearby
        {
            //ROTATE TOWARDS MISSILE-------------------------------------------------------------

            Vector3 direction = target.position - transform.position;
            float directionAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            float currentAngle = (Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg);
            
            if (currentAngle < 0)
                currentAngle += 360;
            if (directionAngle < 0)
                directionAngle += 360;
            
            //if we are not in our buffer range then we move
            if (Mathf.Abs(currentAngle - directionAngle) > 0)
            {

                if (directionAngle < currentAngle)
                {
                    if (Mathf.Abs(currentAngle - directionAngle) < 180)
                        transform.Rotate(new Vector3(0, 0, -1 * angleSpeed * Time.deltaTime));
                    else
                        transform.Rotate(new Vector3(0, 0, angleSpeed * Time.deltaTime));//turn counterclockwise
                    
                }
                else
                {
                    if (Mathf.Abs(currentAngle - directionAngle) < 180)
                        transform.Rotate(new Vector3(0, 0, angleSpeed * Time.deltaTime));//turn counterclockwise
                    else
                        transform.Rotate(new Vector3(0, 0, -1 * angleSpeed * Time.deltaTime));
                }
            }
            
        }//end rotate missile

        //-----------------------------------
        //Move Missile
        //Debug.Log("Move");

        transform.position += transform.up * MissileSpeed * Time.deltaTime;
    }//end update function


    public Transform GrabClosestCollider()
    {

        int index = 0;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, followRadius);
        if (colliders.Length == 0)
        {
            return null;
        }

        float closestDistance = Vector3.Distance(transform.position, colliders[index].gameObject.transform.position);
        for (int i = 1; i < colliders.Length; i++)
        {
            if (closestDistance > Vector3.Distance(transform.position, colliders[i].gameObject.transform.position))
            {
                closestDistance = Vector3.Distance(transform.position, colliders[i].gameObject.transform.position);
                index = i;
            }
        }

        return colliders[index].gameObject.transform;
    }

}
