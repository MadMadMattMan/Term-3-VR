using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] Transform tip;
    [SerializeField] int size;
    [SerializeField] float interpolationStepPercent;
    [SerializeField] Color penColor;
    Color[] colors;
    float tipHeight;
    RaycastHit raycastResult;
    Whiteboard whiteboard;
    Vector2 touchPoint, lastTouchPoint;

    bool touchLastFrame;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tipHeight = tip.localScale.y;
        colors = Enumerable.Repeat(penColor, size * size).ToArray();
    }

    void Update()
    {
        //As when an object is held in vr, it's rb is set to kinematic. So if pen is held, run drawing method.
        if (rb.isKinematic)
            Draw();
    }

    void Draw()
    {
        Debug.DrawRay(tip.position, transform.forward, Color.red, 0.1f);

        //Find out if pen tip is touching the whiteboard
        if (Physics.Raycast(tip.position, transform.forward, out raycastResult, tipHeight))
        {
            //Debug.Log("ray hit");
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
                    return;



                //if the pen was touching the whiteboard last frame
                if (touchLastFrame)
                {
                    //Draw exactly where the pen tip is touching
                    whiteboard.texture.SetPixels(x, y, size, size, colors);

                    //Interpolates the position between frames so line stays solid and not just dots
                    for (float i = 0.01f; i < 1; i += interpolationStepPercent/100)
                    {
                        //Get the interpolated steps
                        int lerpX = (int)Mathf.Lerp(lastTouchPoint.x, x, i);
                        int lerpY = (int)Mathf.Lerp(lastTouchPoint.y, y, i);

                        //Draw an inerpolated dot along the expected pathway
                        whiteboard.texture.SetPixels(lerpX, lerpY, size, size, colors);
                    }

                    //Apply the changes to the texture
                    whiteboard.texture.Apply();
                }

                //Setup for next frame
                lastTouchPoint = new Vector2(x, y);
                touchLastFrame = true;
                return;
            }
        }

        //If pen is released from the whiteboard, reset
        whiteboard = null;
        touchLastFrame = false;
    }
}
