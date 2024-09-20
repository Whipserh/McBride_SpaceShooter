using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        DrawConstellation();
    }


    public void DrawConstellation()
    {
        /**
        for (int i = 0; i < starTransforms.Count - 1; i++)
        {
            //draw line between each position
            Debug.DrawLine(starTransforms[i].position, starTransforms[i + 1].position);
        }
        */

        //current index
        int index = Mathf.FloorToInt(timer / drawingTime) % starTransforms.Count;
        Vector3 direction = starTransforms[index].position - starTransforms[(index+1)%starTransforms.Count].position;
        float directLength = direction.magnitude;//grab the length of the direction
        direction.Normalize();
       Debug.DrawLine(starTransforms[index].position, starTransforms[index].position - direction*directLength*((timer%drawingTime)/drawingTime));
    }
}
