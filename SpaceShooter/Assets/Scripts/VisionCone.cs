using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisionCone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public float sightDistance, visionAngle;
    // Update is called once per frame
    void Update()
    {
        drawVisionCone();
    }

    public void drawVisionCone()
    {
        Vector3 left, right;
        float currentAnlge = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
 

        left = MathUtilities.AngleToVector(currentAnlge + visionAngle / 2, sightDistance);
        right = MathUtilities.AngleToVector(currentAnlge - visionAngle / 2, sightDistance);

        Color detection;
        if (visionConeDetect(target))
        {
            detection = Color.red;
        }
        else
        {
            detection = Color.green;
        }

        Debug.DrawLine(transform.position, transform.position + left, detection);
        Debug.DrawLine(transform.position, transform.position + right, detection);

    }


    public Transform target;

    public bool visionConeDetect(Transform target)
    {
        
        Vector3 directionTarget = target.position - transform.position;
        float directionAngle = Mathf.Atan2(directionTarget.y, directionTarget.x);
        float currentAnlge = Mathf.Atan2(transform.up.y, transform.up.x);


        
        return Vector3.Distance(target.position, transform.position) < sightDistance && Mathf.Abs(currentAnlge - directionAngle)*Mathf.Rad2Deg < visionAngle / 2;
    }
}
