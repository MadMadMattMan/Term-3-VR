using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] Transform tip;
    [SerializeField] int size, interpolationStepPercent;
    [SerializeField] Color penColor;
    Color[] colors;
    float tipHeight;
    RaycastHit raycastResult;
    Whiteboard whiteboard;
    Vector2 touchPoint, lastTouchPoint;

    bool touchLastFrame;


    void Start()
    {
        tipHeight = tip.localScale.y;
        colors = Enumerable.Repeat(penColor, size * size).ToArray();
    }

    void Update()
    {
        Draw();
    }

    void Draw()
    {
        Debug.DrawRay(tip.position, transform.forward, Color.red, 0.1f);

        //Find out if pen tip is touching the whiteboard
        if (Physics.Raycast(tip.position, transform.forward, out raycastResult, tipHeight))
        {
            Debug.Log("ray hit");
            if (raycastResult.transform.CompareTag("Whiteboard Canvas"))
            {
                Debug.Log("ray hit whiteboard");

                //Sets the whiteboard script if is not already set
                if (whiteboard == null)
                {
                    whiteboard = raycastResult.transform.GetComponent<Whiteboard>();
                }

                Debug.Log(whiteboard);

                //Gets a Vector2 position of where the pen tip is touching on the whiteboard
                touchPoint = new Vector2(raycastResult.textureCoord.x, raycastResult.textureCoord.y);

                //Converts this Vector2 position to a pixel coordinate location using the texture

                //XY is problemssahdfkjsadhfkasldfasdflasdjgflsjdfjasdlfasdjfdsfkasdfkjsdhf
                int x = (int)(touchPoint.x * whiteboard.textureSize.x - (size/2));
                int y = (int)(touchPoint.x * whiteboard.textureSize.y - (size/2));
                Debug.Log(x + ", " + y);

                //if pen become out of bounds, stop the script
                if (y < 0 || y > whiteboard.textureSize.y || x < 0 || x > whiteboard.textureSize.x)
                    return;


                if (touchLastFrame)
                {
                    //Draw exactly where the pen tip is touching
                    whiteboard.texture.SetPixels(x, y, size, size, colors);

                    //Interpolates the position between frames in case the marker is moving fast
                    for (int i = 0; i < 100; i += interpolationStepPercent)
                    {
                        //Get the interpolated steps
                        int lerpX = (int)Mathf.Lerp(lastTouchPoint.x, x, i / 100);
                        int lerpY = (int)Mathf.Lerp(lastTouchPoint.y, y, i / 100);

                        //Draw an inerpolated dot along the expected pathway
                        whiteboard.texture.SetPixels(lerpX, lerpY, size, size, colors);
                    }

                    //Apply the changes
                    whiteboard.texture.Apply();
                }


                lastTouchPoint = new Vector2(x, y);
                touchLastFrame = true;
                return;
            }
        }

        whiteboard = null;
        touchLastFrame = false;
    }
}
