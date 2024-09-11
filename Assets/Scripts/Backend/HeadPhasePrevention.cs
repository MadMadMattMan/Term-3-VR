using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class HeadPhasePrevention : MonoBehaviour
{
    [SerializeField] List<Vector3> contactPoints = new List<Vector3>();
    float closestDistanceScaled, maxDistance;
    SphereCollider scollider;
    [SerializeField] RawImage blackout;
    private void Start()
    {
        scollider = GetComponent<SphereCollider>();
        maxDistance = scollider.radius;
        contactPoints.Clear();
    }

    private void Update()
    {
        //Debug.Log(Trial(contact));


        //if there is an objec in range
        if (contactPoints.Count > 0)
        {
            //go through the objects and calculate the closest objects distance
            for (int i = 0; i < contactPoints.Count; i++)
            {
                float k = CalculateAlpha(contactPoints[i]);
                if (k > closestDistanceScaled)
                    closestDistanceScaled = k;
            }
            //after, get the adjusted alpha value for the closest object and set blackout alpha to that 
            blackout.color = new Color(0, 0, 0, closestDistanceScaled);

            //reset closest alpha for next frame
            closestDistanceScaled = 0;
        }
    }

    //When head trigger intersects with a wall, get the x distance between the x of wall or get the z distance between the z of the wall
    
    float CalculateAlpha(Vector3 other)
    {
        Debug.Log("Found Distance for blackout");

        //Gets the distance between the camera and the collided object and always returns positive value
        float dist = Mathf.Abs((other - transform.position).magnitude);
        //gets the distant in relation to the max distance (if max dist away return 0 if min return max)
        float distRelativeMax = dist - maxDistance;

        //Scale relative distance between 0 and 255
        float adjustedAlpha = distRelativeMax *255 / maxDistance;

        Debug.Log(dist + " " + distRelativeMax + " " + adjustedAlpha);

        return adjustedAlpha;
    }


}
