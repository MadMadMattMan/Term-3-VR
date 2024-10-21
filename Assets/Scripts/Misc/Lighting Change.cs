using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightingChange : MonoBehaviour
{
    [SerializeField] Color Default = new Color(255, 237, 186);
    [SerializeField] Color UV = new Color(100, 83, 148);
    [SerializeField] Color fluroYellow = new Color(80, 100, 0);
    Color fluroYellowOff = new Color(80, 100, 0, 0);
    [SerializeField] Color fluroGreen = new Color(121, 254, 12);
    Light mainLight;
    bool isUV = false;
    AudioSource UVbuzz;
    [SerializeField] TextMeshPro[] UVTexts = new TextMeshPro[0];
    [SerializeField] Image[] whiteboardImages;

    private void Start()
    {
        //Sets up the scene so UV light and sound is disabled
        isUV = false;
        mainLight = GetComponent<Light>();

        UVbuzz = GetComponentInChildren<AudioSource>();
        UVbuzz.Stop();

        //Makes the text invisable, set aphla (transparentcy) to 0
        for (int i = 0; i < UVTexts.Length; i++)
        {
            UVTexts[i].alpha = 0f;
        }
        for (int i = 0; i < whiteboardImages.Length; i++)
        {
            whiteboardImages[i].color = fluroYellowOff;
        }
        
    }

    private void FixedUpdate()
    {
        //Debugging tests
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleUV();
        }
        if (isUV)
        {
            UVPulse();
            
        }
    }


    public void ToggleUV()
    {
        //Toggles the light and sound between UV mode and regular mode
        if (mainLight.color == Default)
        {
            mainLight.color = UV;
            isUV = true;
            UVbuzz.Play();
        }
        else
        {
            mainLight.color = Default;
            isUV = false;
            UVbuzz.Stop();

            //Makes the UV text visable
            for (int i = 0; i < UVTexts.Length; i++)
            {
                UVTexts[i].alpha = 0;
                
            }
            for (int i = 0;i < whiteboardImages.Length; i++)
            {
                whiteboardImages[i].color = fluroYellowOff;
            }
        }
    }

    [Header("UV settings")]
    [SerializeField] float strength, range, rate;
    void UVPulse()
    {
        //Pulses light based on a triangle wave function 
        if (mainLight.intensity < 1f || mainLight.intensity > 1 + range)
            rate = -rate;
        //Sets the light intensity
        strength += rate;
        mainLight.intensity = strength + 1;

        //Pulses transparency of the text with the light for effect
        for (int i = 0; i < UVTexts.Length; i++)
        {
            UVTexts[i].alpha = 0.35f + 3*strength;
        }
        for (int i = 0; i < whiteboardImages.Length; i++)
        {
            whiteboardImages[i].color = new Color(fluroYellow.r, fluroYellow.g, fluroYellow.b, 0.15f + 2 * strength);
        }
    }
}
