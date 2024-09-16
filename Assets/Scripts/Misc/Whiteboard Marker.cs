using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] Transform[] tip;
    [SerializeField] int size;
    [SerializeField] float interpolationStepPercent;
    [SerializeField] Color penColor;
    [SerializeField] Whiteboard linkedWhiteboard;

    Color[] colors;
    [SerializeField] float tipHeight;
    RaycastHit raycastResult;
    Whiteboard whiteboard;
    Vector2 touchPoint, lastTouchPoint;

    bool touchLastFrame;

    Rigidbody rb;


    void Start()
    {
        whiteboard = GameObject.FindWithTag("Whiteboard Canvas").GetComponent<Whiteboard>();

        rb = GetComponent<Rigidbody>();
        tipHeight = tip[0].localScale.y;
        if (gameObject.name == "duster")
        {
            penColor = whiteboard.texture.GetPixel(0, 0);
            colors = Enumerable.Repeat(penColor, size * size * 3).ToArray();
        }
        else
            colors = Enumerable.Repeat(penColor, size * size).ToArray();





    }

    void Update()
    {
        //As when an object is held in vr, it's rb is set to kinematic. So if pen is held, run drawing method, also check if pen is touching anything.
        if (rb.isKinematic)
            Draw(0);
    }

    void Draw(int loop)
    {
        for (int i = 0; i < tip.Length; i++)
        {
            if (loop != 0)
                i++;

            Debug.DrawRay(tip[i].position, tip[i].transform.forward, Color.red, 0.01f);

            //Find out if pen tip is touching the whiteboard
            if (Physics.Raycast(tip[i].position, tip[i].transform.forward, out raycastResult, tipHeight))
            {
                Debug.Log("ray hit for " + gameObject.name + " at " + raycastResult.collider.gameObject.name);
                if (raycastResult.transform.CompareTag("Whiteboard Canvas"))
                {
                    ///Debug.Log("ray hit whiteboard");

                    //Sets the whiteboard script if is not already set
                    if (whiteboard == null)
                        whiteboard = raycastResult.transform.GetComponent<Whiteboard>();

                    //Gets a Vector2 position of where the pen tip is touching on the whiteboard
                    touchPoint = new Vector2(raycastResult.textureCoord.x, raycastResult.textureCoord.y);




                    //Converts this Vector2 UV position to a pixel coordinate location using the texture

                    ///Debug.Log("Touch points " + touchPoint.x + ", " + touchPoint.y + "; texture size " + whiteboard.textureSize.x + ", " + whiteboard.textureSize.y + "; pen size" + size);
                    var x = (int)(touchPoint.x * whiteboard.textureSize.x - (size / 2));
                    var y = (int)(touchPoint.y * whiteboard.textureSize.y - (size / 2));
                    ///Debug.Log(x + ", " + y);

                    //if pen leaves the bounds of the whiteboard, stop the script
                    if (y < 0 || y > whiteboard.textureSize.y || x < 0 || x > whiteboard.textureSize.x)
                    {
                        if (loop != tip.Length - 1)
                        {
                            //as i = loop + 1
                            Draw(i);
                        }
                        return;
                    }



                    //if the pen was touching the whiteboard last frame
                    if (touchLastFrame)
                    {
                        //Draw exactly where the pen tip is touching
                        if (gameObject.name == "duster")
                            whiteboard.texture.SetPixels(x, y, size * 3, size, colors);
                        else
                            whiteboard.texture.SetPixels(x, y, size, size, colors);
                        Debug.Log(gameObject.name + " drew on " + whiteboard.gameObject.name);

                        //Interpolates the position between frames so line stays solid and not just dots
                        for (float j = 0.01f; j < 1; j += interpolationStepPercent / 100)
                        {
                            //Get the interpolated steps
                            int lerpX = (int)Mathf.Lerp(lastTouchPoint.x, x, j);
                            int lerpY = (int)Mathf.Lerp(lastTouchPoint.y, y, j);

                            //Draw an inerpolated dot along the expected pathway
                            whiteboard.texture.SetPixels(lerpX, lerpY, size, size, colors);
                        }

                        //Apply the changes to the texture
                        whiteboard.texture.Apply();
                        whiteboard.drawn = true;
                    }

                    //Setup for next frame
                    lastTouchPoint = new Vector2(x, y);
                    touchLastFrame = true;
                    if (loop != tip.Length - 1)
                    {
                        //as i = loop + 1
                        Draw(i);
                    }
                    return;

                }
            }
        }


        //If pen is released from the whiteboard, reset
        whiteboard = null;
        touchLastFrame = false;

    }
}
