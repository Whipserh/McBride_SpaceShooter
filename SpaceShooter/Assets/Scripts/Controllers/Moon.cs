using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(1, 2, orbitedPlanet);
    }

    public Transform orbitedPlanet;
    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        if ((transform.position - target.position).magnitude != radius) {
            transform.position = target.position + radius * Vector3.Normalize(transform.position - target.position);
        }
        //gives us a unit vector based around the origin
        Vector3 direction = transform.position - target.position;
        direction.Normalize();
        
        Vector3 velocity = new Vector3(-direction.y, direction.x) * speed;

        transform.position += velocity * Time.deltaTime;
    }
}
