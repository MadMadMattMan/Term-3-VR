using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightingChange : MonoBehaviour
{
    [SerializeField] Color Default = new Color(255, 237, 186);
    [SerializeField] Color UV = new Color(100, 83, 148);
    [SerializeField] Color FYellow = new Color(80, 100, 0);
    [SerializeField] Color FGellow = new Color(121, 254, 12);
    Light mainLight;
    bool isUV = false;
    AudioSource UVbuzz;
    [SerializeField] TextMeshPro[] UVTexts = new TextMeshPro[0];

    private void Start()
    {
        isUV = false;
        mainLight = GetComponent<Light>();

        UVbuzz = GetComponentInChildren<AudioSource>();
        UVbuzz.Stop();

        for (int i = 0; i < UVTexts.Length; i++)
        {
            UVTexts[i].alpha = 0f;
        }
    }

    private void FixedUpdate()
    {
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
            for (int i = 0; i < UVTexts.Length; i++)
            {
                UVTexts[i].alpha = 0;
            }
        }
    }
    [SerializeField] float strength, range, rate;
    void UVPulse()
    {
        if (mainLight.intensity < 1f || mainLight.intensity > 1 + range)
            rate = -rate;
        strength += rate;
        mainLight.intensity = strength + 1;
        for (int i = 0; i < UVTexts.Length; i++)
        {
            UVTexts[i].alpha = 0.35f + 3*strength;
        }
    }
}
