using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    //Drawing on whiteboard in WhiteboardMarker script
    public Texture2D texture = null;
    public Vector2 textureSize;

    public Material mat;

    public bool drawn = false;

    private void Start()
    {
        //Set up script, creates a new texture image and mat for the
        Renderer renderer = GetComponent<Renderer>();

        if (texture == null)
            texture = new Texture2D((int)textureSize.x, (int)textureSize.y);

        renderer.material.mainTexture = texture;

        mat = new Material(renderer.material);
    }

    private void OnApplicationQuit()
    {
        //Saves the whiteboard state to drive after exit, may be expanded later
        if (drawn)
        {
            byte[] savedImage = texture.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/images.png", savedImage);
        }
    }
}
