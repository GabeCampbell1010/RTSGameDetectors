using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGradient : MonoBehaviour
{
    /// <summary>
    /// detects the terrain gradient that a unit is facing
    /// </summary>

    Vector3 normal = Vector3.up;
    float cosine;
    float cosineDegrees;
    float angle;
    float steepGradient = 10f;

    float timeToGo;

    void Start()
    {
        timeToGo = Time.time + 1.0f;
    }

    private void Update()//
    {

        if (Time.time >= timeToGo)//should do this every second
        {
            Ray ray = new Ray(transform.position, -transform.up);//ray going straight down, shown in red
            Ray forward = new Ray(transform.position, transform.forward);//only used later to determine degree from flat, ray going straight forward shown in blue
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 30f))
            {
                normal = hit.normal;
            }

            //some debug rays for testing
            //Debug.DrawRay(transform.position, -transform.up*10, Color.red, 1f);
            //Debug.DrawRay(transform.position, normal*10, Color.yellow, 1f);
            //Debug.DrawRay(transform.position, transform.forward * 10, Color.blue, 1f);

            normal = transform.InverseTransformDirection(normal);//convert the normal to local space so that we can determine if this particular object is going uphill
            normal = new Vector3(0, normal.y, normal.z);//change normal to only the difference in angle on the z-y plane so its only how steep up you are going as compared with straight ahead
            forward.direction = transform.InverseTransformDirection(forward.direction);//convert forward to local space

            cosine = Vector3.Dot(forward.direction, normal);//math to find angle between straight ahead and non-x portion of normal and convert it into degrees which are a bit more intuitive for humans
            cosineDegrees = Mathf.Acos(cosine);
            angle = cosineDegrees * Mathf.Rad2Deg;
            angle -= 90;//subtract 90 so that flat is zero, negative is downhill and positive is uphill
                        //Debug.Log(angle);

            

            timeToGo = Time.time + .10f;//happens ten times a second, i thought terrain gradient detector shouold be a bit more sensitive than terrain texture detector, but can change these to whatever for performance
            //Debug.Log("Terrain type: " + surfaceIndex);
        }

    }

}
