using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] Transform tip;
    [SerializeField] int size, interpolationStepPercent;

    Renderer renderer;
    Color[] colors;
    float tipHeight;
    RaycastHit raycastResult;
    Whiteboard whiteboard;
    Vector2 touchPoint, lastTouchPoint;

    bool touchLastFrame;


    void Start()
    {
        //Sets up variables
        renderer = GetComponent<Renderer>();
        colors = Enumerable.Repeat(renderer.material.color, size*size).ToArray();
        tipHeight = tip.localScale.y;
    }

    private void Update()
    {
        Draw();
    }

    void Draw()
    {
        //Find out if pen tip is touching the whiteboard
        if (Physics.Raycast(tip.position, transform.up, out raycastResult, tipHeight))
        {
            if (raycastResult.transform.CompareTag("Whiteboard Canvas"))
            {
                //Sets the whiteboard script if is not already set
                if (whiteboard == null)
                {
                    whiteboard = raycastResult.transform.GetComponent<Whiteboard>();
                }

                //Gets a Vector2 position of where the pen tip is touching on the whiteboard
                touchPoint = new Vector2(raycastResult.textureCoord.x, raycastResult.textureCoord.y);

                //Converts this Vector2 position to a pixel coordinate location using the texture
                int x = (int)(touchPoint.x * whiteboard.textureSize.x - (size/2));
                int y = (int)(touchPoint.x * whiteboard.textureSize.y - (size/2));

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
                }
            }
        }
    }
}
